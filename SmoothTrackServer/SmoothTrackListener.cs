using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace SmoothTrack
{
    /// <summary>
    /// Listens to face tracking data sent by the iOS app SmoothTrack via UDP.
    /// See https://apps.apple.com/us/app/smoothtrack-head-tracker/id1528839485
    /// </summary>
    public class SmoothTrackListener
    {
        private readonly CancellationTokenSource _cancelSource;
        private readonly CancellationToken _cancelToken;
        private readonly int _port;

        public SmoothTrackListener(int port)
        {
            _port = port;
            _cancelSource = new CancellationTokenSource();
            _cancelToken = _cancelSource.Token;
        }

        public void Start(Action<FaceTrackingData> callback)
        {
            if (_cancelToken.IsCancellationRequested)
                return;

            Task.Run(() => Listen(callback));
        }

        public void Stop()
        {
            _cancelSource.Cancel();
        }

        private void Listen(Action<FaceTrackingData> callback)
        {
            IPAddress ipAddress =
                Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(addr => (addr.AddressFamily == AddressFamily.InterNetwork)).First();
                // or IPAddress.Any

            UdpClient udpServer = new UdpClient(_port);

            var asDoubles = new double[6];

            while (!_cancelToken.IsCancellationRequested)
            {
                var remoteEndpoint = new IPEndPoint(ipAddress, _port);

                var bytes = udpServer.Receive(ref remoteEndpoint);

                if (bytes.Length != 48)
                    continue;

                Buffer.BlockCopy(bytes, 0, asDoubles, 0, bytes.Length); // TODO Buffer.MemoryCopy directly to FaceTrackingData?
                callback(new FaceTrackingData()
                {
                    X = asDoubles[0],
                    Y = asDoubles[1],
                    Z = asDoubles[2],
                    Yaw   = asDoubles[3],
                    Pitch = asDoubles[4],
                    Roll  = asDoubles[5],
                });

                udpServer.Send(new byte[] { 1 }, 1, remoteEndpoint); // reply back
            }
        }
    }
}

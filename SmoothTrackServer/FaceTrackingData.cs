namespace SmoothTrack
{
    public struct FaceTrackingData
    {
        public double X;
        public double Y;
        public double Z;
        public double Yaw;
        public double Pitch;
        public double Roll;

        public static FaceTrackingData operator +(FaceTrackingData one, FaceTrackingData other) =>
            new FaceTrackingData
            {
                X = one.X + other.X,
                Y = one.Y + other.Y,
                Z = one.Z + other.Z,
                Yaw   = one.Yaw   + other.Yaw,
                Pitch = one.Pitch + other.Pitch,
                Roll  = one.Roll  + other.Roll,
            };

            public static FaceTrackingData operator -(FaceTrackingData one, FaceTrackingData other) =>
            new FaceTrackingData
            {
                X = one.X - other.X,
                Y = one.Y - other.Y,
                Z = one.Z - other.Z,
                Yaw   = one.Yaw   - other.Yaw,
                Pitch = one.Pitch - other.Pitch,
                Roll  = one.Roll  - other.Roll,
            };
    }
}

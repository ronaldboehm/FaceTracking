namespace SmoothTrack
{
    public struct HeadTrackingData
    {
        public double X;
        public double Y;
        public double Z;
        public double Yaw;
        public double Pitch;
        public double Roll;

        public static HeadTrackingData operator +(HeadTrackingData one, HeadTrackingData other) =>
            new HeadTrackingData
            {
                X = one.X + other.X,
                Y = one.Y + other.Y,
                Z = one.Z + other.Z,
                Yaw   = one.Yaw   + other.Yaw,
                Pitch = one.Pitch + other.Pitch,
                Roll  = one.Roll  + other.Roll,
            };

            public static HeadTrackingData operator -(HeadTrackingData one, HeadTrackingData other) =>
            new HeadTrackingData
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

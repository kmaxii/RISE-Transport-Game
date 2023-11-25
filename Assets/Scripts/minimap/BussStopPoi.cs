namespace minimap
{
    public class BussStopPoi : MiniMapPOI
    {
        private StopPoint StopPoint;
        public string StopName => StopPoint.name;
        public string Gid => StopPoint.gid;
    }
}
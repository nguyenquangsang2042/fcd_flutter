using System;
namespace VNASchedule.Bean
{
    public class BeanDeviceInfo : BeanBase
    {
        public string DeviceId { get; set; }
        public string DevicePushToken { get; set; }
        public short DeviceOS { get; set; }
        public string AppVersion { get; set; }
        public string DeviceOSVersion { get; set; }
    }
}

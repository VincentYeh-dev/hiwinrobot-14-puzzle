using RASDK.Basic;
using RASDK.Basic.Message;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExclusiveProgram.device
{
    public class SuckerDevice : IDevice
    {

        public readonly SerialPort serial;
        private readonly int address;

        public bool Connected => serial.IsOpen;

        public SuckerDevice(string device_name)
        {
            serial = new SerialPort(device_name,9600);
        }
        public SuckerDevice()
        {
            serial = new SerialPort(SerialPort.GetPortNames()[0],9600);
        }

        public bool Connect()
        {
            serial.Open();
            return serial.IsOpen;
        }

        public bool Disconnect()
        {
            serial.Close();
            return !serial.IsOpen;
        }

        public void Enable()
        {
            var buffer = new byte[1];
            buffer[0] = 0x01;
            serial.Write(buffer,0,1);
        }
        public void Disable()
        {
            var buffer = new byte[1];
            buffer[0] = 0x00;
            serial.Write(buffer,0,1);
        }
    }
}

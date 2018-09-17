using System;

namespace ProxyGuitar {
    public class USBIP_RET_SUBMIT {

        public int command, seqnum, devid, direction, ep, status, actual_length,
            start_frame, number_of_packets, error_count;
        public UrbSetup urb;
        public byte[] _data;

        public bool ReadyToSend;

        public USBIP_RET_SUBMIT(USBIP_CMD_SUBMIT copy) {
            //Use the same seqnum, devid, direction, ep,
            //transfer flags, start_frame, num_packets, interval, urb always 0
            command = 3;
            seqnum = copy.seqnum;
            devid = copy.devid;
            direction = copy.direction;
            ep = copy.ep;

            status = 0;
            //actual_length
            start_frame = 0;
            number_of_packets = 0;
            error_count = 0;

            urb = new UrbSetup();

            ReadyToSend = false;
        }

        /*
        public USBIP_RET_SUBMIT(byte[] input) {
            bool flip = false;

            command = GTL.ReadInt(input, 0, 4, flip);
            seqnum = GTL.ReadInt(input, 4, 4, flip);
            devid = GTL.ReadInt(input, 8, 4, flip);
            direction = GTL.ReadInt(input, 12, 4, flip);
            ep = GTL.ReadInt(input, 16, 4, flip);
            status = GTL.ReadInt(input, 20, 4, flip);
            actual_length = GTL.ReadInt(input, 24, 4, flip);
            start_frame = GTL.ReadInt(input, 28, 4, flip);
            number_of_packets = GTL.ReadInt(input, 32, 4, flip);
            error_count = GTL.ReadInt(input, 36, 4, flip);

            urb = new UrbSetup(input, 40);

            _data = new byte[actual_length];
            Array.Copy(input, 48, _data, 0, actual_length);

            ReadyToSend = false;
        }
        */

        public byte[] ToBytes() {
            if (_data != null)
                actual_length = _data.Length;
            else
                actual_length = 0;

            byte[] bytes = new byte[48 + actual_length];

            byte[] _command = BitConverter.GetBytes(command);
            byte[] _seqnum = BitConverter.GetBytes(seqnum);
            byte[] _devid = BitConverter.GetBytes(devid);
            byte[] _direction = BitConverter.GetBytes(direction);
            byte[] _ep = BitConverter.GetBytes(ep);
            byte[] _status = BitConverter.GetBytes(status);
            byte[] _actual_length = BitConverter.GetBytes(actual_length);
            byte[] _start_frame = BitConverter.GetBytes(start_frame);
            byte[] _number_of_packets = BitConverter.GetBytes(number_of_packets);
            byte[] _error_count = BitConverter.GetBytes(error_count);

            Array.Reverse(_command);
            Array.Reverse(_seqnum);
            Array.Reverse(_devid);
            Array.Reverse(_direction);
            Array.Reverse(_ep);
            Array.Reverse(_status);
            Array.Reverse(_actual_length);
            Array.Reverse(_number_of_packets);
            Array.Reverse(_error_count);

            Array.Copy(_command, 0, bytes, 0, 4);
            Array.Copy(_seqnum, 0, bytes, 4, 4);
            Array.Copy(_devid, 0, bytes, 8, 4);
            Array.Copy(_direction, 0, bytes, 12, 4);
            Array.Copy(_ep, 0, bytes, 16, 4);
            Array.Copy(_status, 0, bytes, 20, 4);
            Array.Copy(_actual_length, 0, bytes, 24, 4);
            Array.Copy(_start_frame, 0, bytes, 28, 4);
            Array.Copy(_number_of_packets, 0, bytes, 32, 4);
            Array.Copy(_error_count, 0, bytes, 36, 4);
            Array.Copy(urb.ToBytes(), 0, bytes, 40, 8);
            if(actual_length > 0)
                Array.Copy(_data, 0, bytes, 48, actual_length);

            return bytes;
        }
    }
}

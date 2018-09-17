using System;

namespace ProxyGuitar {
    public class USBIP_CMD_SUBMIT {

        public int command, seqnum, devid, direction, ep, transfer_flags,
            transfer_buffer_length, start_frame, number_of_packets, interval;
        public UrbSetup urb;
        public byte[] _remainingURB;

        public USBIP_CMD_SUBMIT(byte[] input) {
            bool flip = false;

            command = GTL.ReadInt(input, 0, 4, flip);
            seqnum = GTL.ReadInt(input, 4, 4, flip);
            devid = GTL.ReadInt(input, 8, 4, flip);
            direction = GTL.ReadInt(input, 12, 4, flip);
            ep = GTL.ReadInt(input, 16, 4, flip);
            transfer_flags = GTL.ReadInt(input, 20, 4, flip);
            transfer_buffer_length = GTL.ReadInt(input, 24, 4, flip);
            start_frame = GTL.ReadInt(input, 28, 4, flip);
            number_of_packets = GTL.ReadInt(input, 32, 4, flip);
            interval = GTL.ReadInt(input, 36, 4, flip);

            urb = new UrbSetup(input, 40);

            if (input.Length > 48) {
                _remainingURB = new byte[input.Length - 48];
                Array.Copy(input, 48, _remainingURB, 0, _remainingURB.Length);
            }
        }

        public byte[] ToBytes() {
            byte[] bytes = new byte[48];

            byte[] _command = BitConverter.GetBytes(command);
            byte[] _seqnum = BitConverter.GetBytes(seqnum);
            byte[] _devid = BitConverter.GetBytes(devid);
            byte[] _direction = BitConverter.GetBytes(direction);
            byte[] _ep = BitConverter.GetBytes(ep);
            byte[] _transfer_flags = BitConverter.GetBytes(transfer_flags);
            byte[] _transfer_buffer_length = BitConverter.GetBytes(transfer_buffer_length);
            byte[] _start_frame = BitConverter.GetBytes(start_frame);
            byte[] _number_of_packets = BitConverter.GetBytes(number_of_packets);
            byte[] _error_count = BitConverter.GetBytes(interval);

            Array.Reverse(_command);
            Array.Reverse(_seqnum);
            Array.Reverse(_devid);
            Array.Reverse(_direction);
            Array.Reverse(_ep);
            Array.Reverse(_transfer_flags);
            Array.Reverse(_transfer_buffer_length);
            Array.Reverse(_number_of_packets);
            Array.Reverse(_error_count);

            Array.Copy(_command, 0, bytes, 0, 4);
            Array.Copy(_seqnum, 0, bytes, 4, 4);
            Array.Copy(_devid, 0, bytes, 8, 4);
            Array.Copy(_direction, 0, bytes, 12, 4);
            Array.Copy(_ep, 0, bytes, 16, 4);
            Array.Copy(_transfer_flags, 0, bytes, 20, 4);
            Array.Copy(_transfer_buffer_length, 0, bytes, 24, 4);
            Array.Copy(_start_frame, 0, bytes, 28, 4);
            Array.Copy(_number_of_packets, 0, bytes, 32, 4);
            Array.Copy(_error_count, 0, bytes, 36, 4);
            Array.Copy(urb.ToBytes(), 0, bytes, 40, 8);

            return bytes;
        }
    }
}

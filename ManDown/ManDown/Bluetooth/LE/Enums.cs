using System;
using System.Collections.Generic;
using System.Text;

namespace ManDown.Bluetooth.LE
{
    // Constants that indicate the current connection state
    public enum STATE
    {
        NONE = 0,            // we're doing nothing
        LISTEN = 1,          // now listening for incoming connections
        CONNECTING = 2,      // now initiating an outgoing connection
        CONNECTED = 3        // now connected to a remote device
    }

    // Message types sent from the BluetoothService Handler
    public enum MESSAGE
    {
        STATE_CHANGE = 1,
        READ = 2,
        WRITE = 3,
        DEVICE_NAME = 4,
        TOAST = 5,
    }
}

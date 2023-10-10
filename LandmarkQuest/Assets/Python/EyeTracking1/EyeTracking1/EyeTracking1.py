import cv2
from gaze_tracking import GazeTracking
import socket
import struct
import threading

# Initialize GazeTracking and webcam
gaze = GazeTracking()
webcam = cv2.VideoCapture(0)

# Create a socket server
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
host = 'localhost'
port = 12345
server_socket.bind((host, port))
server_socket.listen(5)

print("Listening for Unity client on " + host + ":" + str(port))

# List of connected clients
clients = []

# Function to handle a client connection
def handle_client(client_socket):
    try:
        while True:
            _, frame = webcam.read()

            # Refresh gaze tracking and get gaze data
            gaze.refresh(frame)

            # Determine the gaze status (e.g., blinking)
            is_blinking = gaze.is_blinking()

            # Send the gaze status to all connected clients
            for c in clients:
                try:
                    c.sendall(struct.pack("?", is_blinking))
                except socket.error as e:
                    print("Socket error: " + str(e))
                    clients.remove(c)

    except Exception as e:
        print("Client disconnected: " + str(e))
        client_socket.close()

# Accept client connections and spawn a new thread for each client
while True:
    client_socket, addr = server_socket.accept()
    print("Connection from: " + str(addr))
    clients.append(client_socket)

    # Create a new thread to handle the client
    client_handler = threading.Thread(target=handle_client, args=(client_socket,))
    client_handler.start()

# Release the webcam resources
# webcam.release()

# Clean up and close the server socket
server_socket.close()

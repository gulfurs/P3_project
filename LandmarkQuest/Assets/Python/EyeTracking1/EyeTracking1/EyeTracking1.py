import cv2
from gaze_tracking import GazeTracking
import socket
import pickle
import struct
import zlib
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

            # Continue displaying gaze-related information locally
            text = ""
            if is_blinking:
                text = "Blinking"
            elif gaze.is_right():
                text = "Looking right"
            elif gaze.is_left():
                text = "Looking left"
            elif gaze.is_center():
                text = "Looking center"

            cv2.putText(frame, text, (90, 60), cv2.FONT_HERSHEY_DUPLEX, 1.6, (147, 58, 31), 2)

            left_pupil = gaze.pupil_left_coords()
            right_pupil = gaze.pupil_right_coords()
            cv2.putText(frame, "Left pupil:  " + str(left_pupil), (90, 130), cv2.FONT_HERSHEY_DUPLEX, 0.9, (147, 58, 31), 1)
            cv2.putText(frame, "Right pupil: " + str(right_pupil), (90, 165), cv2.FONT_HERSHEY_DUPLEX, 0.9, (147, 58, 31), 1)

            #cv2.imshow("Demo", frame)

            if cv2.waitKey(1) == 27:
                break

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
#webcam.release()

# Clean up and close the server socket
server_socket.close()

# Close all OpenCV windows
cv2.destroyAllWindows()
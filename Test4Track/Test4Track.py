import cv2
import mediapipe as mp
import pyautogui
import socket
import struct

# Create a socket server
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
host = 'localhost'
port = 12345
server_socket.bind((host, port))
server_socket.listen(5)

print("Listening for Unity client on " + host + ":" + str(port))

# Initialize camera
cam = cv2.VideoCapture(0)  # Use 0 for the default camera

# Face Mesh model via mediapipe
face_mesh = mp.solutions.face_mesh.FaceMesh(refine_landmarks=True)  # Gets the face mesh

screen_w, screen_h = pyautogui.size()

# Accept the connection outside the loop
client_socket, addr = server_socket.accept()

# Create a window, but do not display it
cv2.namedWindow('Test4', cv2.WINDOW_NORMAL)
cv2.setWindowProperty('Test4', cv2.WND_PROP_VISIBLE, cv2.WINDOW_NORMAL)

while True:
    _, frame = cam.read()
    frame = cv2.flip(frame, 1)
    rgb_frame = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

    # Process the frame with Face Mesh model
    output = face_mesh.process(rgb_frame)

    # Extract face landmarks
    landmark_points = output.multi_face_landmarks
    frame_h, frame_w, _ = frame.shape

    if landmark_points:
        landmarks = landmark_points[0].landmark

        for id, landmark in enumerate(landmarks[474:478]):
            x = int(landmark.x * frame_w)
            y = int(landmark.y * frame_h)
            cv2.circle(frame, (x, y), 3, (0, 255, 0))

            if id == 1:
                screen_x = screen_w * landmark.x
                screen_y = screen_h * landmark.y
                # pyautogui.moveTo(screen_x, screen_y)

                if screen_x < screen_w / 3:
                    print("Left")
                    client_socket.send(struct.pack("<I", 1))  # Send "Left" message to Unity
                elif screen_x > 2 * screen_w / 3:
                    print("Right")
                    client_socket.send(struct.pack("<I", 2))  # Send "Right" message to Unity
                else:
                    print("Center")
                    client_socket.send(struct.pack("<I", 0))  # Send "Center" message to Unity

    cv2.imshow('Test4', frame)

    if cv2.waitKey(1) & 0xFF == 27:  # Press 'Esc' to exit
        break

cam.release()
cv2.destroyAllWindows()

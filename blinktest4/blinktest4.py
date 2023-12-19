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

# Accept the connection outside the loop
client_socket, addr = server_socket.accept()

#initialize camera
cam = cv2.VideoCapture(0) #use 0 for the default camera

#face Mesh model via mediapipe
face_mesh = mp.solutions.face_mesh.FaceMesh(refine_landmarks=True) #Gets the face mash

screen_w, screen_h = pyautogui.size()

while True:
    _, frame = cam.read()
    frame = cv2.flip(frame, 1)
    rgb_frame = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

    #process the frame with Face Mesh model
    output = face_mesh.process(rgb_frame)

    #extract face landmarks
    landmark_points = output.multi_face_landmarks
    frame_h, frame_w, _ = frame.shape

    if landmark_points:
        landmarks = landmark_points[0].landmark

        right = [landmarks[374], landmarks[387]]
        for landmark in right:
            x = int(landmark.x * frame_w)
            y = int(landmark.y * frame_h)
            cv2.circle(frame, (x, y), 3, (255, 0, 0))
        if (right[0].y - right[1].y) < 0.008:
            client_socket.send(struct.pack("<I", 1))
            print("test right")
        else:
            client_socket.send(struct.pack("<I", 0))

        left = [landmarks[145], landmarks[159]]
        for landmark in left:
            x = int(landmark.x * frame_w)
            y = int(landmark.y * frame_h)
            cv2.circle(frame, (x, y), 3, (0, 255, 255))
        if (left[0].y - left[1].y) < 0.008:
            client_socket.send(struct.pack("<I", 2)) 
            print("test left")
        else:
            client_socket.send(struct.pack("<I", 0)) 

    cv2.imshow('Eye Controlled Mouse', frame)

    if cv2.waitKey(1) & 0xFF == 27:  # Press 'Esc' to exit
        break

cam.release()
cv2.destroyAllWindows()

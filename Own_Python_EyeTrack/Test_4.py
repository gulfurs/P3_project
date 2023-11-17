import mediapipe as mp
import cv2

import pyautogui

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

                
        for id, landmark in enumerate(landmacrks[474:478]):
            x = int(landmark.x * frame_w)
            y = int(landmark.y * frame_h)
            cv2.circle(frame, (x, y), 3, (0, 255, 0))
                
            if id == 1:
                screen_x = screen_w * landmark.x
                screen_y = screen_h * landmark.y
                pyautogui.moveTo(screen_x, screen_y)

                if screen_x < screen_w / 3:
                    print("Left")
                elif screen_x > 2 * screen_w / 3:
                    print("Right")

    cv2.imshow('Eye Controlled Mouse', frame)

    if cv2.waitKey(1) & 0xFF == 27:  # Press 'Esc' to exit
        break

cam.release()
cv2.destroyAllWindows()
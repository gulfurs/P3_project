import cv2
import mediapipe as mp
import pyautogui
import csv
from datetime import datetime

# Initialize camera
cam = cv2.VideoCapture(0)  # Use 0 for the default camera

# Face Mesh model via mediapipe
face_mesh = mp.solutions.face_mesh.FaceMesh(refine_landmarks=True)  # Gets the face mesh

screen_w, screen_h = pyautogui.size()

# Create a window, but do not display it
cv2.namedWindow('Test4', cv2.WINDOW_NORMAL)
cv2.setWindowProperty('Test4', cv2.WND_PROP_VISIBLE, cv2.WINDOW_NORMAL)

current_state = "Center"
pre_state = "Center"

#user_title = input("Enter a title for the CSV file: ")


#title = user_title if user_title else f"eye_states_{datetime.now().strftime('%Y%m%d_%H%M%S')}.csv"
title = f"testperson_{datetime.now().strftime('%Y%m%d_%H%M%S')}.csv"


# Specify the CSV file path
csv_file_path = title

# Open the CSV file for writing
with open(csv_file_path, 'w', newline='') as csvfile:
    fieldnames = ["Frame", "State"]
    writer = csv.DictWriter(csvfile, fieldnames=fieldnames)

    # Write the header
    writer.writeheader()

    frame_count = 0

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

            central_point = landmarks[10]
            x = int(central_point.x * frame_w)
            y = int(central_point.y * frame_h)
            cv2.circle(frame, (x, y), 3, (0, 255, 0))

            screen_x = screen_w * central_point.x
            screen_y = screen_h * central_point.y

            threshold1 = int(screen_w / 4)
            threshold2 = int(screen_w / 8)

            cv2.line(frame, (threshold1, 0), (threshold1, screen_h), (255, 0, 0), 2)
            cv2.line(frame, (threshold2, 0), (threshold2, screen_h), (255, 0, 0), 2)

            if screen_x < screen_w / 3:
                current_state = "Left"
            elif screen_x > 2 * screen_w / 3:
                current_state = "Right"
            else:
                current_state = "Center"

            if current_state != pre_state:
                print(current_state)

                # Write the state and frame count to the CSV file
                writer.writerow({"Frame": frame_count, "State": current_state})

            pre_state = current_state

        cv2.imshow('Test4', frame)

        if cv2.waitKey(1) & 0xFF == 27:  # Press 'Esc' to exit
            break

        frame_count += 1

    # Close the CSV file
    csvfile.close()

cam.release()
cv2.destroyAllWindows()

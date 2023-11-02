import cv2
import dlib
import numpy as np

# Initialize OpenCV and dlib
cap = cv2.VideoCapture(0)
detector = dlib.get_frontal_face_detector()
predictor = dlib.shape_predictor("shape_predictor_68_face_landmarks.dat")

while True:
    ret, frame = cap.read()
    gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)

    faces = detector(gray)
    for face in faces:
        landmarks = predictor(gray, face)

        # Extract eye landmarks
        left_eye_pts = landmarks.parts()[36:42]
        right_eye_pts = landmarks.parts()[42:48]

        # Calculate the center of the pupils
        left_pupil = np.mean(left_eye_pts, axis=0).astype(int)
        right_pupil = np.mean(right_eye_pts, axis=0).astype(int)

        # Draw circles around pupils
        cv2.circle(frame, tuple(left_pupil), 2, (0, 0, 255), -1)
        cv2.circle(frame, tuple(right_pupil), 2, (0, 0, 255), -1)

    cv2.imshow("Gaze Tracking", frame)

    if cv2.waitKey(1) & 0xFF == 27:  # Press 'Esc' to exit
        break

cap.release()
cv2.destroyAllWindows()

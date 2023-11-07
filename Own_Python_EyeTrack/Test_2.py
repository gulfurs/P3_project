import cv2
import numpy as np

#Loading the xml files from the opencv GitHub https://github.com/opencv/opencv/tree/master/data/haarcascades
eye_cascade = cv2.CascadeClassifier('haarcascade_eye.xml')
face_cascade = cv2.CascadeClassifier('haarcascade_frontalface_default.xml')

#Some blob initialization
params = cv2.SimpleBlobDetector_Params()
params.filterByArea = True
params.maxArea = 1500
detector = cv2.SimpleBlobDetector_create(params)

cap = cv2.VideoCapture(0)  #Use 0 for the default camera

def on_threshold_change(x):
    pass
cv2.namedWindow('image')
cv2.createTrackbar('Threshold', 'image', 0, 255, on_threshold_change)

while True:
    ret, frame = cap.read()  # Read a frame from the video capture

    if not ret:
        break

    gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY) #Making the image gray
    faces = face_cascade.detectMultiScale(gray, scaleFactor=1.3, minNeighbors=5, minSize=(30, 30))


    for (x, y, w, h) in faces:
        #Draw a rectangle around the face
        cv2.rectangle(frame, (x, y), (x + w, y + h), (255, 0, 0), 2)

        top_third = y + h // 3

        face_roi = gray[y + h // 24:y + h // 2, x:x + w]
        #gray[y:y + top_third, x:x + w] #region of interest for the eyes in the face
        eyes = eye_cascade.detectMultiScale(face_roi) #Should be the eye detection

        left_eye, right_eye = None, None #If you don't have any eyes

        for (ex, ey, ew, eh) in eyes:
            cv2.rectangle(frame, (x + ex, y + ey), (x + ex + ew, y + ey + eh), (0, 255, 0), 2) #Rectangle around the eye

            #cv2.rectangle(frame, (ex, ey), (ex + ew, ey + eh), (0, 255, 0), 2)

            eye = frame[y + ey:y + ey + eh, x + ex:x + ex + ew]
            threshold = cv2.getTrackbarPos('Threshold', 'image')

            if ey < h / 2:  # Exclude eyes near the top (eyebrows)
                eyecenter = x + ex + ew / 2  # Get the eye center
                if eyecenter < w * 0.5:
                    left_eye = frame[y + ey:y + ey + eh, x + ex:x + ex + ew]
                else:
                    right_eye = frame[y + ey:y + ey + eh, x + ex:x + ex + ew]

            #The thresholding (adjust parameters as needed)
            _, thresholded_eye = cv2.threshold(eye, threshold, 255, cv2.THRESH_BINARY)

            #Im a fish *Blob blob blob*
            keypoints = detector.detect(thresholded_eye)

            for kp in keypoints:
                x_pupil, y_pupil = int(kp.pt[0]) + x + ex, int(kp.pt[1]) + y + ey  # Adjust coordinates to the full frame
                r = int(kp.size / 2)
                cv2.circle(frame, (x_pupil, y_pupil), r, (0, 0, 255), 2)

                left_third = x + ex + ew // 3
                right_third = x + ex + (ew * 2) // 3

                if x_pupil < left_third:
                    print("left")
                elif x_pupil > right_third:
                    print("right")
                else:
                    print("Looking straight ahead")


    cv2.imshow("Eye Tracking", frame)

    if cv2.waitKey(1) & 0xFF == 27:  # Press 'Esc' to exit
        break

cap.release()
cv2.destroyAllWindows()

import cv2 as cv
import numpy as np
import copy

#Loading the xml files from the opencv GitHub https://github.com/opencv/opencv/tree/master/data/haarcascades
face_cascade = cv.CascadeClassifier('haarcascade_frontalface_default.xml')
eye_cascade = cv.CascadeClassifier('haarcascade_eye.xml')

#Some blob initialization
detector_params = cv.SimpleBlobDetector_Params()
detector_params.filterByArea = True
detector_params.maxArea = 1500
detector = cv.SimpleBlobDetector_create(detector_params)

img = cv.imread("Test_img.jpg") #Loading a test Image
img = cv.cvtColor(img, cv.COLOR_RGB2BGR) #Some converting

if img is not None:
    print("ITS WORKING")
else:
    print("No image found - what the flip")
    exit(1)

img_gray = cv.cvtColor(img, cv.COLOR_BGR2GRAY) #Making the image gray

def eye_detect(img, img_gray, classifier):
    gray_copy = img_gray.copy()
    eyes = cascade.detectMultiScale(gray_copy, 1.3, 5) #Should be the eye detection
    width = np.size(img, 1) #Gets the width for the face
    height = np.size(img, 0) #And height
    
    #If no eye is detected
    left_eye = None
    right_eye = None 
    for (x, y, w, h) in coords:
        if y+h > height/2:
            pass
        eyecenter = x + w / 2
        if eye_cascade < width * 0.5:
            left_eye = img[y:y + h, x:x + w]
        else:
            right_eye = img[y:y + h, x:x + w]
    return left_eye, right_eye

def blob_process(img, threshold):
    gray_copy = img_gray.copy()
    _, img = cv2.threshold(gray_copy, threshold, 255, cv2.THRESH_BINARY)
    keypoints = detector.detect(img)
    print(keypoints)
    return keypoints

def cut_eyebrows(img):
    height, width = img.shape[:2]
    eyebrow_h = int(height / 4)
    img = img[eyebrow_h:height, 0:width]  # cut eyebrows out (15 px)
    return img

#Displaying the image
cv.imshow('my image', img)
cv.waitKey(0)
cv.destroyAllWindows()


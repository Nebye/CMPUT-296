import csv, random, sys

def main(datasetSize):
	targetKey = open("key.csv", "wr")
	writerKey = csv.writer(targetKey)
	writerKey.writerow(["data", "center", "userID", "churn"])

	for x in range(0, int(datasetSize)):
		numCenters = random.randint(3, 10)#claim between 2 and 11
		
		#Training Data
		target = open("dataTrain"+str(x)+".csv", "wr")
		writer = csv.writer(target)
		#pellets, fruit, and ghosts, score in thousands
		basicRow = ["userID", "hoursPlayed", "level", "pelletsEaten", "fruitEaten", "ghostsEaten", "avgScore", "maxScore", "totalScore", "churned"]
		writer.writerow(basicRow)

		#Testing Data
		targetTest = open("dataTest"+str(x)+".csv", "wr")
		writerTest = csv.writer(targetTest)
		basicRowTest = ["userID", "hoursPlayed", "level", "pelletsEaten", "fruitEaten", "ghostsEaten", "avgScore", "maxScore", "totalScore"]
		writerTest.writerow(basicRowTest)

		centers = []#Create all the centers
		for center in range(0, numCenters): 
			basicCenter = [0]*len(basicRow)
			#userID
			userID = ""
			for i in range(0, 9):
				userID+=str(random.randint(0,9))

			#hours played
			hoursPlayed = random.randint(1,1000)

			#level
			level = hoursPlayed/random.randint(5,10)
			if level<1:
				level = 1

			#pellets eaten
			pelletsEaten = hoursPlayed*random.randint(20,100)

			#fruit eaten
			fruitEaten = pelletsEaten/random.randint(1, 100)

			fruitEaten = max(fruitEaten, 1)

			#ghosts eaten
			ghostsEaten = pelletsEaten/random.randint(5, 500)

			#total score
			totalScore = pelletsEaten+fruitEaten+ghostsEaten+ random.randint(-10,1000)

			#average score
			avgScore = int(totalScore/float(fruitEaten/random.randint(1,5)))

			#max score
			maxScore = avgScore*random.randint(2,4)+random.randint(-50,1000)

			if maxScore>totalScore:
				maxScore = totalScore/2

			churned = level>totalScore/float(random.randint(400,500))

			churnedStr = ""
			if churned:
				churnedStr = "T"
			else:
				churnedStr = "F"

			centerRow = [userID, hoursPlayed, level, pelletsEaten, fruitEaten, ghostsEaten, avgScore, maxScore, totalScore, churnedStr]
			centers.append(centerRow)

		#Ensure center quality	
		churneds = 0
		for centerRow in centers:
			if centerRow[-1]=="T":
				churneds+=1
			else:
				churneds-=1
		if abs(churneds) == len(centers):
			randomIndex = random.randint(0, len(centers)-1)

			if churneds>0:
				centers[randomIndex][-1] = "F"
			else:
				centers[randomIndex][-1] = "T"

		#Generate datapoints
		dataPoints = []
		while len(dataPoints)<1000:
			randomCenter = random.randint(0, len(centers)-1)
			datapoint = list(centers[randomCenter])

			userID = ""
			for i in range(0, 9):
				userID+=str(random.randint(0,9))

			datapoint[0] = userID

			#hours played
			datapoint[1] = datapoint[1]+random.randint(-200,200)

			if datapoint[1]<1:
				datapoint[1] = 1

			#level
			datapoint[2]+random.randint(-10,10)

			if datapoint[2]<1:
				datapoint[2] = 1

			#pellets eaten
			datapoint[3] = datapoint[1]*random.randint(20,100)

			#fruit eaten
			datapoint[4] = pelletsEaten/random.randint(1, 100)

			datapoint[4] = max(fruitEaten, 1)

			#ghosts eaten
			datapoint[5] = pelletsEaten/random.randint(5, 500)

			#total score
			datapoint[8] = pelletsEaten+fruitEaten+ghostsEaten+ random.randint(-10,1000)

			#average score
			datapoint[6] = int(totalScore/float(fruitEaten/random.randint(1,5)))

			#max score
			datapoint[7] = avgScore*random.randint(2,4)+random.randint(-50,1000)

			#Ensure that the closest center is the right one
			minDist = float("inf")
			closestCenter = -1
			for center in range(0, len(centers)):
				dist = 0
				dist+= float(abs(datapoint[1]-centers[center][1]))/float(500)
				dist+= float(abs(datapoint[2]-centers[center][2]))/float(100)
				dist+= float(abs(datapoint[3]-centers[center][3]))/float(100000)
				dist+= float(abs(datapoint[4]-centers[center][4]))/float(50000)
				dist+= float(abs(datapoint[5]-centers[center][5]))/float(10000)
				dist+= float(abs(datapoint[6]-centers[center][6]))/float(10000)
				dist+= float(abs(datapoint[7]-centers[center][7]))/float(50000)
				dist+= float(abs(datapoint[8]-centers[center][8]))/float(50000)

				if dist<minDist:
					minDist = dist
					closestCenter = center

			if closestCenter ==randomCenter:
				dataPoints.append(datapoint)
				writerKey.writerow([x, closestCenter, datapoint[0], centers[closestCenter][-1]])

		for centerRow in centers:
			dataPoints.append(centerRow)


		while len(dataPoints)>0:
			randomPoint = random.choice(dataPoints)

			writer.writerow(randomPoint)

			dataPoints.remove(randomPoint)

		#Test points
		dataPoints = []
		while len(dataPoints)<100:
			randomCenter = random.randint(0, len(centers)-1)
			datapoint = list(centers[randomCenter])
			datapoint=datapoint[:-1]

			userID = ""
			for i in range(0, 9):
				userID+=str(random.randint(0,9))

			datapoint[0] = userID

			#hours played
			datapoint[1] = datapoint[1]+random.randint(-200,200)

			if datapoint[1]<1:
				datapoint[1] = 1

			#level
			datapoint[2]+random.randint(-10,10)

			if datapoint[2]<1:
				datapoint[2] = 1

			#pellets eaten
			datapoint[3] = datapoint[1]*random.randint(20,100)

			#fruit eaten
			datapoint[4] = pelletsEaten/random.randint(1, 100)

			datapoint[4] = max(fruitEaten, 1)

			#ghosts eaten
			datapoint[5] = pelletsEaten/random.randint(5, 500)

			#total score
			datapoint[8] = pelletsEaten+fruitEaten+ghostsEaten+ random.randint(-10,1000)

			#average score
			datapoint[6] = int(totalScore/float(fruitEaten/random.randint(1,5)))

			#max score
			datapoint[7] = avgScore*random.randint(2,4)+random.randint(-50,1000)

			#Ensure that the closest center is the right one
			minDist = float("inf")
			closestCenter = -1
			for center in range(0, len(centers)):
				dist = 0
				dist+= float(abs(datapoint[1]-centers[center][1]))/float(500)
				dist+= float(abs(datapoint[2]-centers[center][2]))/float(100)
				dist+= float(abs(datapoint[3]-centers[center][3]))/float(100000)
				dist+= float(abs(datapoint[4]-centers[center][4]))/float(50000)
				dist+= float(abs(datapoint[5]-centers[center][5]))/float(10000)
				dist+= float(abs(datapoint[6]-centers[center][6]))/float(10000)
				dist+= float(abs(datapoint[7]-centers[center][7]))/float(50000)
				dist+= float(abs(datapoint[8]-centers[center][8]))/float(50000)

				if dist<minDist:
					minDist = dist
					closestCenter = center

			if closestCenter ==randomCenter:
				dataPoints.append(datapoint)
				writerKey.writerow([x, closestCenter, datapoint[0], centers[closestCenter][-1]])
		for d in dataPoints:
			writerTest.writerow(d)


  
if __name__== "__main__":
	if(len(sys.argv)>1):
		main(sys.argv[1])
	else:
		main(10)


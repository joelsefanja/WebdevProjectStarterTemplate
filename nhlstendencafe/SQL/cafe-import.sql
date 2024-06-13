CREATE DATABASE  IF NOT EXISTS `nhlstendencafe` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `nhlstendencafe`;
-- MySQL dump 10.13  Distrib 8.0.30, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: nhlstendencafe
-- ------------------------------------------------------
-- Server version	8.0.30

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `category` (
  `CategoryId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(128) NOT NULL,
  PRIMARY KEY (`CategoryId`)
) ENGINE=InnoDB AUTO_INCREMENT=54 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` VALUES (1,'Frisdranken'),(3,'Wijnen en aperitieven'),(4,'Warme dranken'),(5,'Speciaal bier'),(10,'Bier'),(53,'Koekjes');
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orderline`
--

DROP TABLE IF EXISTS `orderline`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orderline` (
  `OrderId` int NOT NULL,
  `ProductId` int NOT NULL,
  `Quantity` int NOT NULL,
  `AmountPaid` int NOT NULL,
  PRIMARY KEY (`OrderId`),
  KEY `FK_OrderLineProduct` (`ProductId`),
  KEY `OrderId` (`OrderId`),
  CONSTRAINT `FK_OrderLineOrder` FOREIGN KEY (`OrderId`) REFERENCES `orders` (`OrderId`) ON DELETE CASCADE,
  CONSTRAINT `FK_OrderLineProduct` FOREIGN KEY (`ProductId`) REFERENCES `product` (`ProductId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderline`
--

LOCK TABLES `orderline` WRITE;
/*!40000 ALTER TABLE `orderline` DISABLE KEYS */;
/*!40000 ALTER TABLE `orderline` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orders` (
  `OrderId` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `TableNumber` int NOT NULL,
  `OrderDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`OrderId`),
  KEY `FK_OrdersUser` (`UserId`),
  CONSTRAINT `FK_OrdersUser` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES (1,2,12,'2024-01-03 10:53:00');
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product` (
  `ProductId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(128) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `CategoryId` int NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  PRIMARY KEY (`ProductId`),
  UNIQUE KEY `Name` (`Name`),
  KEY `FK_ProductCategory` (`CategoryId`),
  KEY `ProductId` (`ProductId`),
  CONSTRAINT `FK_ProductCategory` FOREIGN KEY (`CategoryId`) REFERENCES `category` (`CategoryId`) ON DELETE CASCADE,
  CONSTRAINT `product_chk_1` CHECK ((`Price` > 0))
) ENGINE=InnoDB AUTO_INCREMENT=68 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES (5,'Palm',5,3.40),(6,'Hoegaarden witbier',5,3.40),(7,'Hoegaarden Radler 0.0%',5,3.40),(8,'Hoegaarden Radler 2.0%',5,3.40),(9,'Leffe dubbel',5,3.75),(10,'Leffe blond',5,3.75),(11,'Leffe trippel',5,4.25),(12,'Hoegaarden rosé',5,3.50),(13,'Liefmans fruitesse',5,3.50),(14,'Oud bruin',5,2.50),(15,'Biestheuvel blond 6%',5,4.00),(16,'Biestheuvel IPA 7%',5,4.50),(17,'Biestheuvel Tripel 9%',5,4.50),(18,'Coca-cola regular',1,2.30),(19,'Coca-cola light',1,2.30),(20,'Coca-cola zero',1,2.30),(22,'Fanta orange',1,2.30),(23,'Bitter Lemon',1,2.30),(24,'Tonic',1,2.30),(25,'Fanta Cassis',1,2.30),(26,'Chaudfontainte still',1,2.30),(27,'Chaudfontainte sparkling',1,2.30),(28,'Lipton-ice tea regular',1,2.50),(29,'Lipton-ice green',1,2.50),(30,'Appelsap',1,2.50),(31,'Jus d’orange',1,2.50),(32,'Rivella',1,2.50),(33,'Tomatensap',1,2.50),(34,'Chocomel',1,2.50),(35,'Fristi',1,2.50),(36,'Huiswijnen Rood',3,3.75),(37,'Huiswijnen Wit',3,3.75),(38,'Huiswijnen Rose',3,3.75),(39,'Port',3,3.75),(40,'Sherry',3,3.75),(41,'Vermouth',3,3.75),(42,'Kop koffie',4,2.30),(43,'Thee (Lipton)',4,2.30),(44,'Cappuccino',4,2.50),(45,'Latte Macchiato',4,2.50),(46,'Koffie verkeerd',4,2.50),(47,'Espresso',4,2.50),(48,'Warme chocomel',4,3.00),(51,'lekker Appelsapje',4,5.00),(52,'Choco met slagroom',4,2.99),(58,'Biertje 1.1',10,1.99),(61,'Biertje 2.0',10,1.99),(65,'Stroopwafel',53,1.49),(66,'Biertje Sjonnie',10,5.99);
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(255) NOT NULL,
  `PasswordHash` varchar(255) NOT NULL,
  `FirstName` varchar(255) DEFAULT NULL,
  `LastName` varchar(255) DEFAULT NULL,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (2,'joelsefanja@gmail.com','$2b$10$AK9SfzeNuZ0p3e7T9vivY.d9o5hFCW5DZ6OtMzF5Ht0W0t.6xsAoC','Joël','van Geest','2023-12-28 11:09:11'),(5,'sjonnie@gmail.com','$2b$10$s.jCQ5RqKrqmmdvdcSuemubuM0zo1QhAdgljf1wQMHdeijpbkc8sW','Joël','van Geest','2024-05-17 13:07:28');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-06-13  9:58:29

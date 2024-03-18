CREATE DATABASE  IF NOT EXISTS `coach_connect` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `coach_connect`;
-- MySQL dump 10.13  Distrib 8.0.30, for Win64 (x86_64)
--
-- Host: localhost    Database: coach_connect
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
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20240314135814_CoachInitial','8.0.2');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `coaches`
--

DROP TABLE IF EXISTS `coaches`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `coaches` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `FirstName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LastName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `HashedPassword` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Salt` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `coaches`
--

LOCK TABLES `coaches` WRITE;
/*!40000 ALTER TABLE `coaches` DISABLE KEYS */;
INSERT INTO `coaches` VALUES ('0a95b9b1-6fb7-42a7-8333-56e649a48fe7','Sarah','Williams','23456789','sarah@hotmail.com','$2a$11$BbgQ4HusNxyg.3HeUPU6d.JF7Kf2jy5IcK5oETdah84MDeSIBx9Ku','$2a$11$BbgQ4HusNxyg.3HeUPU6d.','2024-03-06 14:29:14.121637','2024-03-06 14:29:14.121637'),('2b1e02fc-4b92-4b0d-84a7-2418ff07ac13','Petter','Edderkopp','65352635','koppen@gmail.com','$2a$11$n10AmiNhS3jg60ob56f1k.Ej/4w8sFn8xk1vvQsFWwbyqhi0jWegS','$2a$11$n10AmiNhS3jg60ob56f1k.','2024-03-06 14:19:48.199406','2024-03-06 14:19:48.199406'),('5b60c5a7-590b-4c9d-95e3-4648eb83300d','Alex','Smith','32103210','alex@outlook.com','$2a$11$R2UJtUw4UXnuYqNn3rSJXO1C5p9ueSfuKSLiAVYFyUkec2E7JkONK','$2a$11$R2UJtUw4UXnuYqNn3rSJXO','2024-03-06 14:26:34.073548','2024-03-15 12:37:48.381852'),('92a93093-c123-4748-a8d9-558d61690d76','Mikkel','Rev','87878787','mike@hotmail.com','$2a$11$GOHnFWFkY0DJXtMTZWb7NOOD7XyxIsQiowJOeOWqlubgRQhvD3H/6','$2a$11$GOHnFWFkY0DJXtMTZWb7NO','2024-03-06 14:19:03.097361','2024-03-06 14:19:03.097361'),('9b5b194a-f273-4ab2-a87d-620b8898e473','Ole Gunnar','Solskjær','72635543','sol@epost.com','$2a$11$B3TpobbGNwqqqORTGaWkG.1/tuDc2QLjC0e.b12.s8lLg9qQSlTRC','$2a$11$B3TpobbGNwqqqORTGaWkG.','2024-03-06 14:20:54.504202','2024-03-06 14:20:54.504202'),('a5ffaa7c-3933-47c3-b2b4-6c7e242e2288','Emma','Lee','65436544','emma@hotmail.com','$2a$11$ZiGvq0UL7/ELtApkJ8KK6eyuPPbyLmVKFBzKJmzk9ZGZzzIX5VKIu','$2a$11$ZiGvq0UL7/ELtApkJ8KK6e','2024-03-06 14:25:01.321233','2024-03-06 14:25:01.321233'),('a6c5b4d3-2fcb-4e9a-a457-03e9af6d28e6','Sophie','Miller','56782345','sophie@hotmail.com','$2a$11$Xpspbd4yegDKKdo3v5PY2uhXvC4ngDPX0PrW0k1/48H2GJj/2n7f2','$2a$11$Xpspbd4yegDKKdo3v5PY2u','2024-03-06 14:31:04.194171','2024-03-06 14:31:04.194171'),('acd5a6b8-89c5-40a9-87a5-2a097e02b9ed','Lisa','Johnson','78675645','lisa@yahoo.no','$2a$11$.KrHnHRap8Zv.zvG1ltHr.PpyBxu/4v6l4GmNK4G6bQv1TX9qX2.O','$2a$11$.KrHnHRap8Zv.zvG1ltHr.','2024-03-06 14:27:26.583999','2024-03-06 14:27:26.583999'),('b1f5ef4e-c48c-4db3-a10b-07d65da0614b','Chris','Brown','87654321','chris@yahoo.com','$2a$11$2.E9V0VRslIqC3S1G5ld9O1cMnH4LWCeX9kQsDhPWNO2dJxkG3/ai','$2a$11$2.E9V0VRslIqC3S1G5ld9O','2024-03-06 14:23:24.710713','2024-03-06 14:23:24.710713'),('b2b4c1b0-faf6-4949-9977-95d6a81f2ab8','Mike','Taylor','56789012','mike@msn.no','$2a$11$zrC0mBQ1MksBs/rr78DwgekI4ehbpmelXOzXW6yFXeL4hZsZmdoXu','$2a$11$zrC0mBQ1MksBs/rr78Dwge','2024-03-06 14:28:21.114488','2024-03-06 14:28:21.114488'),('b6c7d8e9-1a2b-3c4d-5e6f-7a8b9c0d1e2f','Anna','Taylor','78901234','anna@yahoo.com','$2a$11$aAKiwKfi3IBhELGAY8szvOVW8ozB5XUjGJwJj9j3UVqNh3d2.zVwC','$2a$11$aAKiwKfi3IBhELGAY8szvO','2024-03-06 14:32:43.087937','2024-03-06 14:32:43.087937'),('bb4ef059-9657-48f7-a13c-2bb29725de1b','Martin','Ottesen','90908776','ottis@epost.no','$2a$11$J0to0OIKqC7oF1KplyL3CeZyDmHIpyKbYP4Uvd0GS.RBucPT15aX2','$2a$11$J0to0OIKqC7oF1KplyL3Ce','2024-03-06 14:21:49.178045','2024-03-06 14:21:49.178045'),('be7fb570-8794-4f13-94c2-8b9e2d8b137d','Lucas','Garcia','98761234','lucas@hotmail.com','$2a$11$9YVUGVa.Cqz.E2LdAlhrLO7o9B2ijRFI6WT3PpVXLjdxrnhEIRiSK','$2a$11$9YVUGVa.Cqz.E2LdAlhrLO','2024-03-06 14:24:08.152317','2024-03-06 14:24:08.152317'),('c1d2e3f4-a5b6-7c8d-9e0f-1a2b3c4d5e6f','John','Lee','89012345','john@gmail.com','$2a$11$4RpvFAsZJK.T1.f0wwQr4usUNbUSPajF5fIeB3vVUc0/Jlgtw1OO.','$2a$11$4RpvFAsZJK.T1.f0wwQr4u','2024-03-06 14:33:33.543623','2024-03-06 14:33:33.543623'),('d3e4f5a6-b7c8-9d0e-1f2a-3b4c5d6e7f89','Emily','White','90123456','emily@hotmail.com','$2a$11$8.7TIGXxhF2R3trYPp5v4e0aQpDT6UCIeQCP1Dpft5PFd3pHkFMB6','$2a$11$8.7TIGXxhF2R3trYPp5v4e','2024-03-06 14:34:21.905444','2024-03-06 14:34:21.905444'),('dabb4b1e-e928-4957-b381-1544e42b8f88','Linda','Smith','98765432','linda@gmail.com','$2a$11$8VOjxWddq1ywTYBlJ0hacuXhso/qdRSg4JmtkGowu.2byJ/X0gX1S','$2a$11$8VOjxWddq1ywTYBlJ0hacu','2024-03-06 14:22:35.364122','2024-03-06 14:22:35.364122'),('e00927cb-a616-43d3-a82e-21439469ef13','Quyen','Ho','43434343','quyen@hotmail.com','$2a$11$FuwPcUhXRx13gsjD1wL1LuktH99KsHo9JWjzGbxPvIlK/hpPRxVXG','$2a$11$FuwPcUhXRx13gsjD1wL1Lu','2024-03-06 14:17:15.522361','2024-03-06 14:17:15.522361'),('e2c3d4e7-bd4b-45ab-95d5-9fd6294f2237','Jenny','Clark','12345678','jenny@gmail.com','$2a$11$qXfdp.9JUUjVabAVQGgMWuq4PAHnys9Kmt3CzwG/3F7zLEcK4Sv1G','$2a$11$qXfdp.9JUUjVabAVQGgMWu','2024-03-06 14:25:45.708754','2024-03-06 14:25:45.708754'),('e736c4f5-fc24-4be1-b7de-d8532d90cd2d','Kevin','Wilson','45671234','kevin@gmail.com','$2a$11$9bJfzv0m6tz2SzJXNX7XieWzq8CpdEw9VmWVqX7tnQ7M84o/LKJXq','$2a$11$9bJfzv0m6tz2SzJXNX7Xie','2024-03-06 14:30:08.957222','2024-03-06 14:30:08.957222'),('f3e2d1c0-ba9c-4ff1-a3f5-b6de37b8c9c7','David','Martinez','23456789','david@gmail.com','$2a$11$NblVPp4YhWWj4MxYsX6zvOp2UPXaYMPsv7IZTkZtSWWkpCz2XM/kq','$2a$11$NblVPp4YhWWj4MxYsX6zvO','2024-03-06 14:31:57.619093','2024-03-06 14:31:57.619093');
/*!40000 ALTER TABLE `coaches` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `game_attendences`
--

DROP TABLE IF EXISTS `game_attendences`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `game_attendences` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `GameId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `PlayerId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Game_attendences_GameId` (`GameId`),
  KEY `IX_Game_attendences_PlayerId` (`PlayerId`),
  CONSTRAINT `FK_Game_attendences_Games_GameId` FOREIGN KEY (`GameId`) REFERENCES `games` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Game_attendences_Players_PlayerId` FOREIGN KEY (`PlayerId`) REFERENCES `players` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `game_attendences`
--

LOCK TABLES `game_attendences` WRITE;
/*!40000 ALTER TABLE `game_attendences` DISABLE KEYS */;
INSERT INTO `game_attendences` VALUES ('8215514a-c2f8-46fd-a547-ab5c1fc76004','2f042e86-d75e-4591-a810-aca808cdd0d9','12345678-1234-1234-1234-123456789abc','2024-03-11 10:01:45.179495','2024-03-11 10:01:45.179495'),('8415514a-c2f8-46fd-a547-ab5c1fc76111','2f042e86-d75e-4591-a810-aca808cdd0d9','65445678-1234-1234-1234-1234567ccbba','2024-03-11 10:01:45.179495','2024-03-11 10:01:45.179495'),('8615514a-c2f8-46fd-a547-ab5c1fc76222','2f042e86-d75e-4591-a810-aca808cdd0d9','87654321-1234-1234-1234-123456789111','2024-03-14 15:01:45.179495','2024-03-14 15:01:45.179495'),('8815514a-c2f8-46fd-a547-ab5c1fc76333','2f042e86-d75e-4591-a810-aca808cdd0d9','87654321-2345-2345-2345-123456789222','2024-03-16 12:01:45.179495','2024-03-16 12:01:45.179495'),('9015514a-c2f8-46fd-a547-ab5c1fc76444','2f042e86-d75e-4591-a810-aca808cdd0d9','87654321-3456-3456-3456-123456789333','2024-03-19 14:01:45.179495','2024-03-19 14:01:45.179495'),('9215514a-c2f8-46fd-a547-ab5c1fc76555','2f042e86-d75e-4591-a810-aca80812cde3','87654321-4567-4567-4567-123456789444','2024-03-22 11:01:45.179495','2024-03-22 11:01:45.179495'),('9415514a-c2f8-46fd-a547-ab5c1fc76666','2f042e86-d75e-4591-a810-aca80812cde3','87654321-5678-5678-5678-123456789555','2024-03-25 10:01:45.179495','2024-03-25 10:01:45.179495'),('9615514a-c2f8-46fd-a547-ab5c1fc76777','2f042e86-d75e-4591-a810-aca80812cde3','87654321-6789-6789-6789-123456789666','2024-03-28 13:01:45.179495','2024-03-28 13:01:45.179495'),('9815514a-c2f8-46fd-a547-ab5c1fc76888','2f042e86-d75e-4591-a810-aca80812cde3','12345678-1234-1234-1234-123456789abc','2024-04-01 11:01:45.179495','2024-04-01 11:01:45.179495'),('a015514a-c2f8-46fd-a547-ab5c1fc76999','2f042e86-d75e-4591-a810-aca80812cde3','65445678-1234-1234-1234-1234567ccbba','2024-04-05 15:01:45.179495','2024-04-05 15:01:45.179495'),('a215514a-c2f8-46fd-a547-ab5c1fc76a10','2f042e86-d75e-4591-a810-aca80872a930','87654321-4567-4567-4567-123456789444','2024-04-09 12:01:45.179495','2024-04-09 12:01:45.179495'),('a415514a-c2f8-46fd-a547-ab5c1fc76b11','2f042e86-d75e-4591-a810-aca80872a930','65445678-1234-1234-1234-1234567ccbba','2024-04-13 10:01:45.179495','2024-04-13 10:01:45.179495'),('a615514a-c2f8-46fd-a547-ab5c1fc76c12','2f042e86-d75e-4591-a810-aca80872a930','87654321-5678-5678-5678-123456789177','2024-04-17 15:01:45.179495','2024-04-17 15:01:45.179495'),('a815514a-c2f8-46fd-a547-ab5c1fc76d13','2f042e86-d75e-4591-a810-aca80872a930','87654321-6789-6789-6789-123456789188','2024-04-21 10:01:45.179495','2024-04-21 10:01:45.179495'),('aa15514a-c2f8-46fd-a547-ab5c1fc76e14','2f042e86-d75e-4591-a810-aca80872ccc7','87654321-7890-7890-7890-123456789000','2024-04-25 12:01:45.179495','2024-04-25 12:01:45.179495'),('ac15514a-c2f8-46fd-a547-ab5c1fc76f15','2f042e86-d75e-4591-a810-aca80872ccc7','87654321-8901-8901-8901-123456789cb2','2024-04-29 11:01:45.179495','2024-04-29 11:01:45.179495'),('ae15514a-c2f8-46fd-a547-ab5c1fc76a16','2f042e86-d75e-4591-a810-aca80872bcf1','87654321-6789-6789-6789-123456789666','2024-05-03 13:01:45.179495','2024-05-03 13:01:45.179495'),('b015514a-c2f8-46fd-a547-ab5c1fc76b17','2f042e86-d75e-4591-a810-aca80872bcf1','87654321-1234-1234-1234-123456789111','2024-05-07 14:01:45.179495','2024-05-07 14:01:45.179495'),('b215514a-c2f8-46fd-a547-ab5c1fc76c18','2f042e86-d75e-4591-a810-aca80872bcf1','87654321-4567-4567-4567-123456789444','2024-05-11 15:01:45.179495','2024-05-11 15:01:45.179495'),('b415514a-c2f8-46fd-a547-ab5c1fc76d19','2f042e86-d75e-4591-a810-aca80872bcf1','65445678-1234-1234-1234-1234567ccbba','2024-05-15 16:01:45.179495','2024-05-15 16:01:45.179495');
/*!40000 ALTER TABLE `game_attendences` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `games`
--

DROP TABLE IF EXISTS `games`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `games` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Location` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `OpponentName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `GameTime` datetime(6) NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `games`
--

LOCK TABLES `games` WRITE;
/*!40000 ALTER TABLE `games` DISABLE KEYS */;
INSERT INTO `games` VALUES ('2f042e86-d75e-4591-a810-aca80812cde3','Bergen','Bergen Ballklubb','2024-03-16 11:00:49.312000','2024-03-16 12:00:29.081990','2024-03-16 12:00:29.081990'),('2f042e86-d75e-4591-a810-aca808725555','Bærum','Bærum Bears','2024-04-17 13:00:49.312000','2024-04-17 14:00:29.081990','2024-04-17 14:00:29.081990'),('2f042e86-d75e-4591-a810-aca808726531','Sandefjord','Sandefjord FK','2024-12-06 09:30:49.312000','2024-03-11 11:00:29.081990','2024-03-11 11:00:29.081990'),('2f042e86-d75e-4591-a810-aca80872653c','Bergen','Brann FK','2024-12-06 09:30:49.312000','2024-03-11 11:00:29.081990','2024-03-11 11:00:29.081990'),('2f042e86-d75e-4591-a810-aca80872654a','Arendal','Arendal Avengers','2024-04-05 14:00:49.312000','2024-04-05 15:00:29.081990','2024-04-05 15:00:29.081990'),('2f042e86-d75e-4591-a810-aca808726666','Sarpsborg','Sarpsborg Spartans','2024-04-21 10:30:49.312000','2024-04-21 11:00:29.081990','2024-04-21 11:00:29.081990'),('2f042e86-d75e-4591-a810-aca808726b02','Molde','Molde Mavericks','2024-04-13 09:30:49.312000','2024-04-13 10:00:29.081990','2024-04-13 10:00:29.081990'),('2f042e86-d75e-4591-a810-aca808727777','Drammen','Drammen Dragons','2024-04-25 12:45:49.312000','2024-04-25 13:30:29.081990','2024-04-25 13:30:29.081990'),('2f042e86-d75e-4591-a810-aca808728888','Akershus','Akershus Archers','2024-04-29 09:00:49.312000','2024-04-29 10:00:29.081990','2024-04-29 10:00:29.081990'),('2f042e86-d75e-4591-a810-aca808729999','Nordstrand','Nordstrand Navigators','2024-05-03 15:30:49.312000','2024-05-03 16:00:29.081990','2024-05-03 16:00:29.081990'),('2f042e86-d75e-4591-a810-aca80872a0b1','Bodø','Bodø Bandits','2024-04-09 11:15:49.312000','2024-04-09 12:00:29.081990','2024-04-09 12:00:29.081990'),('2f042e86-d75e-4591-a810-aca80872a930','Oslo','Oslo United','2024-03-14 14:30:49.312000','2024-03-14 15:00:29.081990','2024-03-14 15:00:29.081990'),('2f042e86-d75e-4591-a810-aca80872aaa5','Stavanger','Stavanger Strikers','2024-03-22 10:15:49.312000','2024-03-22 11:00:29.081990','2024-03-22 11:00:29.081990'),('2f042e86-d75e-4591-a810-aca80872bbb6','Tromsø','Tromsø Tornadoes','2024-03-25 09:00:49.312000','2024-03-25 10:00:29.081990','2024-03-25 10:00:29.081990'),('2f042e86-d75e-4591-a810-aca80872bcf1','Rogaland','Rogaland Rockets','2024-05-07 11:15:49.312000','2024-05-07 12:00:29.081990','2024-05-07 12:00:29.081990'),('2f042e86-d75e-4591-a810-aca80872ccc7','Kristiansand','Kristiansand Kickers','2024-03-28 12:30:49.312000','2024-03-28 13:30:29.081990','2024-03-28 13:30:29.081990'),('2f042e86-d75e-4591-a810-aca80872f104','Trondheim','Trondheim Tigers','2024-03-19 13:45:49.312000','2024-03-19 14:30:29.081990','2024-03-19 14:30:29.081990'),('2f042e86-d75e-4591-a810-aca808cdd0d4','Moss','Moss FK','2024-12-11 08:59:49.312000','2024-03-11 10:00:29.081990','2024-03-11 10:00:29.081990'),('2f042e86-d75e-4591-a810-aca808cdd0d9','Moss','Moss FK','2024-03-11 08:59:49.312000','2024-03-11 10:00:29.081990','2024-03-11 10:00:29.081990'),('2f042e86-d75e-4591-a810-aca808dddd81','Fredrikstad','Fredrikstad Falcons','2024-04-01 10:45:49.312000','2024-04-01 11:30:29.081990','2024-04-01 11:30:29.081990'),('58015fcc-b194-44a0-8c1e-e7b1036dfb1d','Brazil','Ceara','2024-06-15 12:28:56.643000','2024-03-15 13:30:20.914987','2024-03-15 13:30:20.914987');
/*!40000 ALTER TABLE `games` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `logs`
--

DROP TABLE IF EXISTS `logs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `logs` (
  `id` int NOT NULL AUTO_INCREMENT,
  `Timestamp` varchar(100) DEFAULT NULL,
  `Level` varchar(15) DEFAULT NULL,
  `Template` text,
  `Message` text,
  `Exception` text,
  `Properties` text,
  `_ts` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `logs`
--

LOCK TABLES `logs` WRITE;
/*!40000 ALTER TABLE `logs` DISABLE KEYS */;
INSERT INTO `logs` VALUES (1,'2024-03-18 10:42:09.775+01:00','Debug','Getting coaches','Getting coaches',NULL,'{\"SourceContext\":\"CoachConnect.API.Controllers.CoachesController\",\"ActionId\":\"23285ab7-8413-423d-bd33-c802297b6146\",\"ActionName\":\"CoachConnect.API.Controllers.CoachesController.GetCoaches (CoachConnect.API)\",\"RequestId\":\"0HN275UKHCKV6:00000009\",\"RequestPath\":\"/api/v1/coaches\",\"ConnectionId\":\"0HN275UKHCKV6\"}','2024-03-18 09:42:19'),(2,'2024-03-18 10:42:09.861+01:00','Debug','Getting all coaches','Getting all coaches',NULL,'{\"SourceContext\":\"CoachConnect.BusinessLayer.Services.CoachService\",\"ActionId\":\"23285ab7-8413-423d-bd33-c802297b6146\",\"ActionName\":\"CoachConnect.API.Controllers.CoachesController.GetCoaches (CoachConnect.API)\",\"RequestId\":\"0HN275UKHCKV6:00000009\",\"RequestPath\":\"/api/v1/coaches\",\"ConnectionId\":\"0HN275UKHCKV6\"}','2024-03-18 09:42:19'),(3,'2024-03-18 10:42:09.920+01:00','Debug','Getting coaches from db','Getting coaches from db',NULL,'{\"SourceContext\":\"CoachConnect.DataAccess.Repositories.CoachRepository\",\"ActionId\":\"23285ab7-8413-423d-bd33-c802297b6146\",\"ActionName\":\"CoachConnect.API.Controllers.CoachesController.GetCoaches (CoachConnect.API)\",\"RequestId\":\"0HN275UKHCKV6:00000009\",\"RequestPath\":\"/api/v1/coaches\",\"ConnectionId\":\"0HN275UKHCKV6\"}','2024-03-18 09:42:19'),(4,'2024-03-18 10:42:11.832+01:00','Warning','The query uses a row limiting operator (\'Skip\'/\'Take\') without an \'OrderBy\' operator. This may lead to unpredictable results. If the \'Distinct\' operator is used after \'OrderBy\', then make sure to use the \'OrderBy\' operator after \'Distinct\' as the ordering would otherwise get erased.','The query uses a row limiting operator (\'Skip\'/\'Take\') without an \'OrderBy\' operator. This may lead to unpredictable results. If the \'Distinct\' operator is used after \'OrderBy\', then make sure to use the \'OrderBy\' operator after \'Distinct\' as the ordering would otherwise get erased.',NULL,'{\"EventId\":{\"Id\":10102,\"Name\":\"Microsoft.EntityFrameworkCore.Query.RowLimitingOperationWithoutOrderByWarning\"},\"SourceContext\":\"Microsoft.EntityFrameworkCore.Query\",\"ActionId\":\"23285ab7-8413-423d-bd33-c802297b6146\",\"ActionName\":\"CoachConnect.API.Controllers.CoachesController.GetCoaches (CoachConnect.API)\",\"RequestId\":\"0HN275UKHCKV6:00000009\",\"RequestPath\":\"/api/v1/coaches\",\"ConnectionId\":\"0HN275UKHCKV6\"}','2024-03-18 09:42:19'),(5,'2024-03-18 10:42:11.840+01:00','Warning','The query uses a row limiting operator (\'Skip\'/\'Take\') without an \'OrderBy\' operator. This may lead to unpredictable results. If the \'Distinct\' operator is used after \'OrderBy\', then make sure to use the \'OrderBy\' operator after \'Distinct\' as the ordering would otherwise get erased.','The query uses a row limiting operator (\'Skip\'/\'Take\') without an \'OrderBy\' operator. This may lead to unpredictable results. If the \'Distinct\' operator is used after \'OrderBy\', then make sure to use the \'OrderBy\' operator after \'Distinct\' as the ordering would otherwise get erased.',NULL,'{\"EventId\":{\"Id\":10102,\"Name\":\"Microsoft.EntityFrameworkCore.Query.RowLimitingOperationWithoutOrderByWarning\"},\"SourceContext\":\"Microsoft.EntityFrameworkCore.Query\",\"ActionId\":\"23285ab7-8413-423d-bd33-c802297b6146\",\"ActionName\":\"CoachConnect.API.Controllers.CoachesController.GetCoaches (CoachConnect.API)\",\"RequestId\":\"0HN275UKHCKV6:00000009\",\"RequestPath\":\"/api/v1/coaches\",\"ConnectionId\":\"0HN275UKHCKV6\"}','2024-03-18 09:42:19'),(6,'2024-03-18 10:42:12.519+01:00','Information','HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms','HTTP \"GET\" \"/api/v1/coaches\" responded 200 in 3507.9899 ms',NULL,'{\"RequestMethod\":\"GET\",\"RequestPath\":\"/api/v1/coaches\",\"StatusCode\":200,\"Elapsed\":3507.9899,\"SourceContext\":\"Serilog.AspNetCore.RequestLoggingMiddleware\",\"RequestId\":\"0HN275UKHCKV6:00000009\",\"ConnectionId\":\"0HN275UKHCKV6\"}','2024-03-18 09:42:19'),(7,'2024-03-18 10:43:51.043+01:00','Information','HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms','HTTP \"POST\" \"/api/v1/coaches/register\" responded 400 in 798.4948 ms',NULL,'{\"RequestMethod\":\"POST\",\"RequestPath\":\"/api/v1/coaches/register\",\"StatusCode\":400,\"Elapsed\":798.4948,\"SourceContext\":\"Serilog.AspNetCore.RequestLoggingMiddleware\",\"RequestId\":\"0HN275VGKMQE2:00000009\",\"ConnectionId\":\"0HN275VGKMQE2\"}','2024-03-18 09:43:53'),(8,'2024-03-18 10:44:34.001+01:00','Information','HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms','HTTP \"POST\" \"/api/v1/coaches/register\" responded 400 in 20.5644 ms',NULL,'{\"RequestMethod\":\"POST\",\"RequestPath\":\"/api/v1/coaches/register\",\"StatusCode\":400,\"Elapsed\":20.5644,\"SourceContext\":\"Serilog.AspNetCore.RequestLoggingMiddleware\",\"RequestId\":\"0HN275VGKMQE2:0000000B\",\"ConnectionId\":\"0HN275VGKMQE2\"}','2024-03-18 09:44:43'),(9,'2024-03-18 10:44:46.097+01:00','Debug','Registering new coach: {email}','Registering new coach: \"string@tester.no\"',NULL,'{\"email\":\"string@tester.no\",\"SourceContext\":\"CoachConnect.API.Controllers.CoachesController\",\"ActionId\":\"62bd5a08-34a8-4a04-9156-122a02412213\",\"ActionName\":\"CoachConnect.API.Controllers.CoachesController.RegisterCoach (CoachConnect.API)\",\"RequestId\":\"0HN275VGKMQE2:0000000D\",\"RequestPath\":\"/api/v1/coaches/register\",\"ConnectionId\":\"0HN275VGKMQE2\"}','2024-03-18 09:44:53'),(10,'2024-03-18 10:44:46.213+01:00','Debug','Registering new coach: {email}','Registering new coach: \"string@tester.no\"',NULL,'{\"email\":\"string@tester.no\",\"SourceContext\":\"CoachConnect.BusinessLayer.Services.CoachService\",\"ActionId\":\"62bd5a08-34a8-4a04-9156-122a02412213\",\"ActionName\":\"CoachConnect.API.Controllers.CoachesController.RegisterCoach (CoachConnect.API)\",\"RequestId\":\"0HN275VGKMQE2:0000000D\",\"RequestPath\":\"/api/v1/coaches/register\",\"ConnectionId\":\"0HN275VGKMQE2\"}','2024-03-18 09:44:53'),(11,'2024-03-18 10:44:46.217+01:00','Debug','Getting coach by email: {email} from db','Getting coach by email: \"string@tester.no\" from db',NULL,'{\"email\":\"string@tester.no\",\"SourceContext\":\"CoachConnect.DataAccess.Repositories.CoachRepository\",\"ActionId\":\"62bd5a08-34a8-4a04-9156-122a02412213\",\"ActionName\":\"CoachConnect.API.Controllers.CoachesController.RegisterCoach (CoachConnect.API)\",\"RequestId\":\"0HN275VGKMQE2:0000000D\",\"RequestPath\":\"/api/v1/coaches/register\",\"ConnectionId\":\"0HN275VGKMQE2\"}','2024-03-18 09:44:53'),(12,'2024-03-18 10:44:48.700+01:00','Debug','Adding coach: {coach} to db','Adding coach: \"string@tester.no\" to db',NULL,'{\"coach\":\"string@tester.no\",\"SourceContext\":\"CoachConnect.DataAccess.Repositories.CoachRepository\",\"ActionId\":\"62bd5a08-34a8-4a04-9156-122a02412213\",\"ActionName\":\"CoachConnect.API.Controllers.CoachesController.RegisterCoach (CoachConnect.API)\",\"RequestId\":\"0HN275VGKMQE2:0000000D\",\"RequestPath\":\"/api/v1/coaches/register\",\"ConnectionId\":\"0HN275VGKMQE2\"}','2024-03-18 09:44:53'),(13,'2024-03-18 10:44:49.105+01:00','Information','HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms','HTTP \"POST\" \"/api/v1/coaches/register\" responded 200 in 3032.6353 ms',NULL,'{\"RequestMethod\":\"POST\",\"RequestPath\":\"/api/v1/coaches/register\",\"StatusCode\":200,\"Elapsed\":3032.6353,\"SourceContext\":\"Serilog.AspNetCore.RequestLoggingMiddleware\",\"RequestId\":\"0HN275VGKMQE2:0000000D\",\"ConnectionId\":\"0HN275VGKMQE2\"}','2024-03-18 09:44:53'),(14,'2024-03-18 10:50:25.258+01:00','Debug','Getting coaches','Getting coaches',NULL,'{\"SourceContext\":\"CoachConnect.API.Controllers.CoachesController\",\"ActionId\":\"eb089d58-28a6-4cb9-b9a1-b7b9d4950ca3\",\"ActionName\":\"CoachConnect.API.Controllers.CoachesController.GetCoaches (CoachConnect.API)\",\"RequestId\":\"0HN27634V9Q33:00000009\",\"RequestPath\":\"/api/v1/coaches\",\"ConnectionId\":\"0HN27634V9Q33\"}','2024-03-18 09:50:34'),(15,'2024-03-18 10:50:25.333+01:00','Debug','Getting all coaches','Getting all coaches',NULL,'{\"SourceContext\":\"CoachConnect.BusinessLayer.Services.CoachService\",\"ActionId\":\"eb089d58-28a6-4cb9-b9a1-b7b9d4950ca3\",\"ActionName\":\"CoachConnect.API.Controllers.CoachesController.GetCoaches (CoachConnect.API)\",\"RequestId\":\"0HN27634V9Q33:00000009\",\"RequestPath\":\"/api/v1/coaches\",\"ConnectionId\":\"0HN27634V9Q33\"}','2024-03-18 09:50:34'),(16,'2024-03-18 10:50:25.360+01:00','Debug','Getting coaches from db','Getting coaches from db',NULL,'{\"SourceContext\":\"CoachConnect.DataAccess.Repositories.CoachRepository\",\"ActionId\":\"eb089d58-28a6-4cb9-b9a1-b7b9d4950ca3\",\"ActionName\":\"CoachConnect.API.Controllers.CoachesController.GetCoaches (CoachConnect.API)\",\"RequestId\":\"0HN27634V9Q33:00000009\",\"RequestPath\":\"/api/v1/coaches\",\"ConnectionId\":\"0HN27634V9Q33\"}','2024-03-18 09:50:34'),(17,'2024-03-18 10:50:26.694+01:00','Warning','The query uses a row limiting operator (\'Skip\'/\'Take\') without an \'OrderBy\' operator. This may lead to unpredictable results. If the \'Distinct\' operator is used after \'OrderBy\', then make sure to use the \'OrderBy\' operator after \'Distinct\' as the ordering would otherwise get erased.','The query uses a row limiting operator (\'Skip\'/\'Take\') without an \'OrderBy\' operator. This may lead to unpredictable results. If the \'Distinct\' operator is used after \'OrderBy\', then make sure to use the \'OrderBy\' operator after \'Distinct\' as the ordering would otherwise get erased.',NULL,'{\"EventId\":{\"Id\":10102,\"Name\":\"Microsoft.EntityFrameworkCore.Query.RowLimitingOperationWithoutOrderByWarning\"},\"SourceContext\":\"Microsoft.EntityFrameworkCore.Query\",\"ActionId\":\"eb089d58-28a6-4cb9-b9a1-b7b9d4950ca3\",\"ActionName\":\"CoachConnect.API.Controllers.CoachesController.GetCoaches (CoachConnect.API)\",\"RequestId\":\"0HN27634V9Q33:00000009\",\"RequestPath\":\"/api/v1/coaches\",\"ConnectionId\":\"0HN27634V9Q33\"}','2024-03-18 09:50:34'),(18,'2024-03-18 10:50:26.699+01:00','Warning','The query uses a row limiting operator (\'Skip\'/\'Take\') without an \'OrderBy\' operator. This may lead to unpredictable results. If the \'Distinct\' operator is used after \'OrderBy\', then make sure to use the \'OrderBy\' operator after \'Distinct\' as the ordering would otherwise get erased.','The query uses a row limiting operator (\'Skip\'/\'Take\') without an \'OrderBy\' operator. This may lead to unpredictable results. If the \'Distinct\' operator is used after \'OrderBy\', then make sure to use the \'OrderBy\' operator after \'Distinct\' as the ordering would otherwise get erased.',NULL,'{\"EventId\":{\"Id\":10102,\"Name\":\"Microsoft.EntityFrameworkCore.Query.RowLimitingOperationWithoutOrderByWarning\"},\"SourceContext\":\"Microsoft.EntityFrameworkCore.Query\",\"ActionId\":\"eb089d58-28a6-4cb9-b9a1-b7b9d4950ca3\",\"ActionName\":\"CoachConnect.API.Controllers.CoachesController.GetCoaches (CoachConnect.API)\",\"RequestId\":\"0HN27634V9Q33:00000009\",\"RequestPath\":\"/api/v1/coaches\",\"ConnectionId\":\"0HN27634V9Q33\"}','2024-03-18 09:50:34'),(19,'2024-03-18 10:50:27.255+01:00','Information','HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms','HTTP \"GET\" \"/api/v1/coaches\" responded 200 in 2482.8986 ms',NULL,'{\"RequestMethod\":\"GET\",\"RequestPath\":\"/api/v1/coaches\",\"StatusCode\":200,\"Elapsed\":2482.8986,\"SourceContext\":\"Serilog.AspNetCore.RequestLoggingMiddleware\",\"RequestId\":\"0HN27634V9Q33:00000009\",\"ConnectionId\":\"0HN27634V9Q33\"}','2024-03-18 09:50:34'),(20,'2024-03-18 10:50:47.088+01:00','Debug','Deleting coach: {id}','Deleting coach: 2db896b5-69da-44d7-8f68-c8ab3a46ae86',NULL,'{\"id\":\"2db896b5-69da-44d7-8f68-c8ab3a46ae86\",\"SourceContext\":\"CoachConnect.API.Controllers.CoachesController\",\"ActionId\":\"442e4ca2-ad83-47e2-b815-c0d354d5633e\",\"ActionName\":\"CoachConnect.API.Controllers.CoachesController.DeleteCoach (CoachConnect.API)\",\"RequestId\":\"0HN27634V9Q33:0000000B\",\"RequestPath\":\"/api/v1/coaches/2db896b5-69da-44d7-8f68-c8ab3a46ae86\",\"ConnectionId\":\"0HN27634V9Q33\"}','2024-03-18 09:50:54'),(21,'2024-03-18 10:50:47.098+01:00','Debug','Deleting coach: {id}','Deleting coach: \"CoachId { coachId = 2db896b5-69da-44d7-8f68-c8ab3a46ae86 }\"',NULL,'{\"id\":\"CoachId { coachId = 2db896b5-69da-44d7-8f68-c8ab3a46ae86 }\",\"SourceContext\":\"CoachConnect.BusinessLayer.Services.CoachService\",\"ActionId\":\"442e4ca2-ad83-47e2-b815-c0d354d5633e\",\"ActionName\":\"CoachConnect.API.Controllers.CoachesController.DeleteCoach (CoachConnect.API)\",\"RequestId\":\"0HN27634V9Q33:0000000B\",\"RequestPath\":\"/api/v1/coaches/2db896b5-69da-44d7-8f68-c8ab3a46ae86\",\"ConnectionId\":\"0HN27634V9Q33\"}','2024-03-18 09:50:54'),(22,'2024-03-18 10:50:47.102+01:00','Debug','Deleting coach: {id} from db','Deleting coach: \"CoachId { coachId = 2db896b5-69da-44d7-8f68-c8ab3a46ae86 }\" from db',NULL,'{\"id\":\"CoachId { coachId = 2db896b5-69da-44d7-8f68-c8ab3a46ae86 }\",\"SourceContext\":\"CoachConnect.DataAccess.Repositories.CoachRepository\",\"ActionId\":\"442e4ca2-ad83-47e2-b815-c0d354d5633e\",\"ActionName\":\"CoachConnect.API.Controllers.CoachesController.DeleteCoach (CoachConnect.API)\",\"RequestId\":\"0HN27634V9Q33:0000000B\",\"RequestPath\":\"/api/v1/coaches/2db896b5-69da-44d7-8f68-c8ab3a46ae86\",\"ConnectionId\":\"0HN27634V9Q33\"}','2024-03-18 09:50:54'),(23,'2024-03-18 10:50:47.481+01:00','Information','HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms','HTTP \"DELETE\" \"/api/v1/coaches/2db896b5-69da-44d7-8f68-c8ab3a46ae86\" responded 200 in 414.8061 ms',NULL,'{\"RequestMethod\":\"DELETE\",\"RequestPath\":\"/api/v1/coaches/2db896b5-69da-44d7-8f68-c8ab3a46ae86\",\"StatusCode\":200,\"Elapsed\":414.8061,\"SourceContext\":\"Serilog.AspNetCore.RequestLoggingMiddleware\",\"RequestId\":\"0HN27634V9Q33:0000000B\",\"ConnectionId\":\"0HN27634V9Q33\"}','2024-03-18 09:50:54');
/*!40000 ALTER TABLE `logs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `players`
--

DROP TABLE IF EXISTS `players`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `players` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `FirstName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LastName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TeamId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Players_TeamId` (`TeamId`),
  KEY `IX_Players_UserId` (`UserId`),
  CONSTRAINT `FK_Players_Teams_TeamId` FOREIGN KEY (`TeamId`) REFERENCES `teams` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Players_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `players`
--

LOCK TABLES `players` WRITE;
/*!40000 ALTER TABLE `players` DISABLE KEYS */;
INSERT INTO `players` VALUES ('12345678-1234-1234-1234-123456789abc','John','Doe','20065784-cdb9-465a-a439-6a627c448ca8','ee57d1c3-b41b-4be8-b45e-14f2a25b1001','2024-03-05 12:00:00.000000','2024-03-05 12:00:00.000000'),('65445678-1234-1234-1234-1234567ccbba','Martin','Ødegård','9ff9eab0-57ef-4530-ad29-1ebee9f682c3','d7e21b6a-c28c-47e8-8b0b-5a93ffbbf002','2024-03-05 12:00:00.000000','2024-03-05 12:00:00.000000'),('87654321-0123-0123-0123-123456789122','Lars','Pedersen','5c053d1b-6c70-4d3b-a030-35286a978b7a','e5d5e6a2-e5e4-4d79-a3c5-ccfd36f7c012','2024-03-06 14:52:00.000000','2024-03-06 14:52:00.000000'),('87654321-1234-1234-1234-123456789111','Anders','Hansen','f15a1513-eb40-4ca3-b8bb-c06959e1d6b5','a8f93c52-d1b0-4e46-a8f8-22ab9c50d003','2024-03-06 14:43:00.000000','2024-03-06 14:43:00.000000'),('87654321-1234-1234-1234-123456789133','Hanna','Solberg','2e88d66f-1d63-4bc2-90b5-0700458748ef','d3b5a3d1-e0f2-4bf6-a5c3-7e8d9f1a2013','2024-03-06 14:53:00.000000','2024-03-06 14:53:00.000000'),('87654321-2345-2345-2345-123456789144','Max','Johansson','820504a9-01cd-4812-b915-495e26fe0fa1','e1a2b3d4-f5b6-4f3d-a8e6-9b0c1d2e3014','2024-03-06 14:54:00.000000','2024-03-06 14:54:00.000000'),('87654321-2345-2345-2345-123456789222','Lisa','Bakken','fdaca55a-2ed0-4760-b1ea-ead99086d1d7','b2d84d4e-921c-4c17-af43-18d13b105004','2024-03-06 14:44:00.000000','2024-03-06 14:44:00.000000'),('87654321-3456-3456-3456-123456789155','Emma','Haugen','9d3de077-bb47-42ff-b695-170e6d08743a','f3c4e1a2-b3d5-46e8-b5c2-7f8d9e0a1015','2024-03-06 14:55:00.000000','2024-03-06 14:55:00.000000'),('87654321-3456-3456-3456-123456789333','Mohammed','Ali','20065784-cdb9-465a-a439-6a627c448787','da6d3453-70b9-487d-b31e-f0f3d75fe005','2024-03-06 14:45:00.000000','2024-03-06 14:45:00.000000'),('87654321-4567-4567-4567-123456789166','Oliver','Sivertsen','48a9d05a-8b21-46d8-8714-8aa73a46c4e5','d1b2c3a4-e5f6-4b2d-98a1-b3c4d5e6f016','2024-03-06 14:56:00.000000','2024-03-06 14:56:00.000000'),('87654321-4567-4567-4567-123456789444','Anne','Johansen','9ff9eab0-57ef-4530-ad29-1ebee9f686a6','b408f10b-cde8-4ecb-91c2-7a5669bb7006','2024-03-06 14:46:00.000000','2024-03-06 14:46:00.000000'),('87654321-5678-5678-5678-123456789177','Ida','Gundersen','36ab0d4f-d7e1-43a7-85cd-b487cf74f3b8','7d8e9f0a-1b2c-3d4e-5f6a-7b8c9d0e1017','2024-03-06 14:57:00.000000','2024-03-06 14:57:00.000000'),('87654321-5678-5678-5678-123456789555','Sofie','Berg','f15a1513-eb40-4ca3-b8bb-c06959e1d009','c04d382e-69e8-49ef-bc28-1f8d2ab2c007','2024-03-06 14:47:00.000000','2024-03-06 14:47:00.000000'),('87654321-6789-6789-6789-123456789188','Mia','Lund','2bf14738-6f12-4af0-8e1b-7d79789b993f','8e9f0a1b-2c3d-4e5f-6a7b-8c9d0e1f2018','2024-03-06 14:58:00.000000','2024-03-06 14:58:00.000000'),('87654321-6789-6789-6789-123456789666','Fredrik','Pettersen','fdaca55a-2ed0-4760-b1ea-ead99086dccc','dab3e709-5dfb-4d40-b5e7-3fe17ff2b008','2024-03-06 14:48:00.000000','2024-03-06 14:48:00.000000'),('87654321-7890-7890-7890-123456789000','Alexander','Eide','12345678-90ab-cdef-1234-567890abcdef','9f0a1b2c-3d4e-5f6a-7b8c-9d0e1f2a3019','2024-03-06 14:59:00.000000','2024-03-06 14:59:00.000000'),('87654321-7890-7890-7890-123456789777','Nora','Andreassen','27c2097f-2d6d-44b4-9ecf-1b96dab3ab3d','b01b6b08-2f43-4be5-b40b-7b9fd2d3d009','2024-03-06 14:49:00.000000','2024-03-06 14:49:00.000000'),('87654321-8901-8901-8901-123456789888','Marius','Kristiansen','d9e4e229-7738-4d26-822d-1b13fb1052c9','c9c7d687-946d-426f-9364-c6e1c3d2b010','2024-03-06 14:50:00.000000','2024-03-06 14:50:00.000000'),('87654321-8901-8901-8901-123456789cb2','Sarah','Thomsen','22222222-2222-2222-2222-222222222222','0a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4020','2024-03-06 15:00:00.000000','2024-03-06 15:00:00.000000'),('87654321-9012-9012-9012-123456789999','Emilie','Larsen','eb7f4ad3-132d-4345-b5f3-b41762fe61f9','a3b2a7e5-b0e2-40e2-a42d-69e10a22d011','2024-03-06 14:51:00.000000','2024-03-06 14:51:00.000000');
/*!40000 ALTER TABLE `players` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `practice_attendences`
--

DROP TABLE IF EXISTS `practice_attendences`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `practice_attendences` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `PracticeId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `PlayerId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Practice_attendences_PlayerId` (`PlayerId`),
  KEY `IX_Practice_attendences_PracticeId` (`PracticeId`),
  CONSTRAINT `FK_Practice_attendences_Players_PlayerId` FOREIGN KEY (`PlayerId`) REFERENCES `players` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Practice_attendences_Practices_PracticeId` FOREIGN KEY (`PracticeId`) REFERENCES `practices` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `practice_attendences`
--

LOCK TABLES `practice_attendences` WRITE;
/*!40000 ALTER TABLE `practice_attendences` DISABLE KEYS */;
INSERT INTO `practice_attendences` VALUES ('9215514a-c2f8-46fd-a547-ab5c1fc76005','2f042e86-d75e-4591-a810-aca808726501','12345678-1234-1234-1234-123456789abc','2024-07-06 08:01:00.000000','2024-07-06 08:01:00.000000'),('9415514a-c2f8-46fd-a547-ab5c1fc76012','2f042e86-d75e-4591-a810-aca808726702','65445678-1234-1234-1234-1234567ccbba','2024-07-07 09:01:00.000000','2024-07-07 09:01:00.000000'),('9615514a-c2f8-46fd-a547-ab5c1fc76021','2f042e86-d75e-4591-a810-aca808726603','87654321-3456-3456-3456-123456789333','2024-07-08 10:01:00.000000','2024-07-08 10:01:00.000000'),('9815514a-c2f8-46fd-a547-ab5c1fc76035','2f042e86-d75e-4591-a810-aca808726604','87654321-5678-5678-5678-123456789555','2024-07-09 11:01:00.000000','2024-07-09 11:01:00.000000'),('9a15514a-c2f8-46fd-a547-ab5c1fc76044','2f042e86-d75e-4591-a810-aca808726405','87654321-9012-9012-9012-123456789999','2024-07-10 12:01:00.000000','2024-07-10 12:01:00.000000'),('9a15514a-c2f8-46fd-a547-ab5c1fc76055','2f042e86-d75e-4591-a810-aca808726405','87654321-5678-5678-5678-123456789555','2024-07-23 15:01:00.000000','2024-07-23 15:01:00.000000'),('9b15514a-c2f8-46fd-a547-ab5c1fc76066','2f042e86-d75e-4591-a810-aca808726405','12345678-1234-1234-1234-123456789abc','2024-07-24 16:01:00.000000','2024-07-24 16:01:00.000000'),('9c15514a-c2f8-46fd-a547-ab5c1fc76052','2f042e86-d75e-4591-a810-aca808726501','87654321-2345-2345-2345-123456789144','2024-07-11 13:01:00.000000','2024-07-11 13:01:00.000000'),('9c15514a-c2f8-46fd-a547-ab5c1fc76078','2f042e86-d75e-4591-a810-aca808726405','87654321-3456-3456-3456-123456789333','2024-07-25 17:01:00.000000','2024-07-25 17:01:00.000000'),('9e15514a-c2f8-46fd-a547-ab5c1fc76063','2f042e86-d75e-4591-a810-aca808726501','87654321-3456-3456-3456-123456789333','2024-07-12 14:01:00.000000','2024-07-12 14:01:00.000000'),('9g15514a-c2f8-46fd-a547-ab5c1fc76074','2f042e86-d75e-4591-a810-aca808726501','87654321-6789-6789-6789-123456789188','2024-07-13 15:01:00.000000','2024-07-13 15:01:00.000000'),('9i15514a-c2f8-46fd-a547-ab5c1fc76081','2f042e86-d75e-4591-a810-aca808726501','87654321-8901-8901-8901-123456789cb2','2024-07-14 16:01:00.000000','2024-07-14 16:01:00.000000'),('9k15514a-c2f8-46fd-a547-ab5c1fc76093','2f042e86-d75e-4591-a810-aca808726702','87654321-3456-3456-3456-123456789333','2024-07-15 17:01:00.000000','2024-07-15 17:01:00.000000'),('9m15514a-c2f8-46fd-a547-ab5c1fc76098','2f042e86-d75e-4591-a810-aca808726702','87654321-5678-5678-5678-123456789555','2024-07-16 08:01:00.000000','2024-07-16 08:01:00.000000'),('9o15514a-c2f8-46fd-a547-ab5c1fc760a1','2f042e86-d75e-4591-a810-aca808726702','87654321-1234-1234-1234-123456789133','2024-07-17 09:01:00.000000','2024-07-17 09:01:00.000000'),('9q15514a-c2f8-46fd-a547-ab5c1fc760b0','2f042e86-d75e-4591-a810-aca808726603','87654321-2345-2345-2345-123456789144','2024-07-18 10:01:00.000000','2024-07-18 10:01:00.000000'),('9s15514a-c2f8-46fd-a547-ab5c1fc760c3','2f042e86-d75e-4591-a810-aca808726603','87654321-7890-7890-7890-123456789000','2024-07-19 11:01:00.000000','2024-07-19 11:01:00.000000'),('9u15514a-c2f8-46fd-a547-ab5c1fc760d4','2f042e86-d75e-4591-a810-aca808726604','87654321-6789-6789-6789-123456789188','2024-07-20 12:01:00.000000','2024-07-20 12:01:00.000000'),('9w15514a-c2f8-46fd-a547-ab5c1fc760e5','2f042e86-d75e-4591-a810-aca808726604','87654321-9012-9012-9012-123456789999','2024-07-21 13:01:00.000000','2024-07-21 13:01:00.000000'),('9y15514a-c2f8-46fd-a547-ab5c1fc760f6','2f042e86-d75e-4591-a810-aca808726405','65445678-1234-1234-1234-1234567ccbba','2024-07-22 14:01:00.000000','2024-07-22 14:01:00.000000');
/*!40000 ALTER TABLE `practice_attendences` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `practices`
--

DROP TABLE IF EXISTS `practices`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `practices` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Location` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PracticeDate` datetime(6) NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `practices`
--

LOCK TABLES `practices` WRITE;
/*!40000 ALTER TABLE `practices` DISABLE KEYS */;
INSERT INTO `practices` VALUES ('2f042e86-d75e-4591-a810-aca808726405','Tromsø','2024-07-10 12:00:00.000000','2024-07-10 12:00:00.000000','2024-07-10 12:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726501','Oslo','2024-07-06 08:00:00.000000','2024-07-06 08:00:00.000000','2024-07-06 08:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726603','Trondheim','2024-07-08 10:00:00.000000','2024-07-08 10:00:00.000000','2024-07-08 10:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726604','Stavanger','2024-07-09 11:00:00.000000','2024-07-09 11:00:00.000000','2024-07-09 11:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726702','Bergen','2024-07-07 09:00:00.000000','2024-07-07 09:00:00.000000','2024-07-07 09:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726a06','Kristiansand','2024-07-11 13:00:00.000000','2024-07-11 13:00:00.000000','2024-07-11 13:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726aa2','Lillehammer','2024-07-17 09:00:00.000000','2024-07-17 09:00:00.000000','2024-07-17 09:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726ac8','Kristiansund','2024-07-23 15:00:00.000000','2024-07-23 15:00:00.000000','2024-07-23 15:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726b07','Fredrikstad','2024-07-12 14:00:00.000000','2024-07-12 14:00:00.000000','2024-07-12 14:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726bb3','Drammen','2024-07-18 10:00:00.000000','2024-07-18 10:00:00.000000','2024-07-18 10:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726c08','Arendal','2024-07-13 15:00:00.000000','2024-07-13 15:00:00.000000','2024-07-13 15:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726ca9','Moss','2024-07-24 16:00:00.000000','2024-07-24 16:00:00.000000','2024-07-24 16:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726cc4','Skien','2024-07-19 11:00:00.000000','2024-07-19 11:00:00.000000','2024-07-19 11:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726d09','Bodø','2024-07-14 16:00:00.000000','2024-07-14 16:00:00.000000','2024-07-14 16:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726dd5','Ålesund','2024-07-20 12:00:00.000000','2024-07-20 12:00:00.000000','2024-07-20 12:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726e10','Molde','2024-07-15 17:00:00.000000','2024-07-15 17:00:00.000000','2024-07-15 17:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726ee6','Hamar','2024-07-21 13:00:00.000000','2024-07-21 13:00:00.000000','2024-07-21 13:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726f11','Haugesund','2024-07-16 08:00:00.000000','2024-07-16 08:00:00.000000','2024-07-16 08:00:00.000000'),('2f042e86-d75e-4591-a810-aca808726ff7','Sarpsborg','2024-07-22 14:00:00.000000','2024-07-22 14:00:00.000000','2024-07-22 14:00:00.000000'),('2f042e86-d75e-4591-a810-aca80872af20','Sandefjord','2024-07-25 17:00:00.000000','2024-07-25 17:00:00.000000','2024-07-25 17:00:00.000000');
/*!40000 ALTER TABLE `practices` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `teams`
--

DROP TABLE IF EXISTS `teams`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `teams` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CoachId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TeamCity` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TeamName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Teams_CoachId` (`CoachId`),
  CONSTRAINT `FK_Teams_Coaches_CoachId` FOREIGN KEY (`CoachId`) REFERENCES `coaches` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `teams`
--

LOCK TABLES `teams` WRITE;
/*!40000 ALTER TABLE `teams` DISABLE KEYS */;
INSERT INTO `teams` VALUES ('0a1b2c3d-4e5f-6a7b-8c9d-0e1f2a3b4020','d3e4f5a6-b7c8-9d0e-1f2a-3b4c5d6e7f89','Boston','Warriors','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('7d8e9f0a-1b2c-3d4e-5f6a-7b8c9d0e1017','f3e2d1c0-ba9c-4ff1-a3f5-b6de37b8c9c7','Seattle','Dragons','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('8e9f0a1b-2c3d-4e5f-6a7b-8c9d0e1f2018','b6c7d8e9-1a2b-3c4d-5e6f-7a8b9c0d1e2f','Denver','Knights','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('9f0a1b2c-3d4e-5f6a-7b8c-9d0e1f2a3019','c1d2e3f4-a5b6-7c8d-9e0f-1a2b3c4d5e6f','Washington','Titans','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('a3b2a7e5-b0e2-40e2-a42d-69e10a22d011','5b60c5a7-590b-4c9d-95e3-4648eb83300d','Austin','Pumas','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('a8f93c52-d1b0-4e46-a8f8-22ab9c50d003','9b5b194a-f273-4ab2-a87d-620b8898e473','Chicago','Panthers','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('b01b6b08-2f43-4be5-b40b-7b9fd2d3d009','a5ffaa7c-3933-47c3-b2b4-6c7e242e2288','Dallas','Lakers','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('b2d84d4e-921c-4c17-af43-18d13b105004','bb4ef059-9657-48f7-a13c-2bb29725de1b','Houston','Bears','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('b408f10b-cde8-4ecb-91c2-7a5669bb7006','dabb4b1e-e928-4957-b381-1544e42b8f88','Philadelphia','Eagles','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('c04d382e-69e8-49ef-bc28-1f8d2ab2c007','b1f5ef4e-c48c-4db3-a10b-07d65da0614b','San Antonio','Hawks','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('c9c7d687-946d-426f-9364-c6e1c3d2b010','e2c3d4e7-bd4b-45ab-95d5-9fd6294f2237','San Jose','Cougars','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('d1b2c3a4-e5f6-4b2d-98a1-b3c4d5e6f016','a6c5b4d3-2fcb-4e9a-a457-03e9af6d28e6','Charlotte','Sharks','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('d3b5a3d1-e0f2-4bf6-a5c3-7e8d9f1a2013','b2b4c1b0-faf6-4949-9977-95d6a81f2ab8','Indianapolis','Cheetahs','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('d7e21b6a-c28c-47e8-8b0b-5a93ffbbf002','92a93093-c123-4748-a8d9-558d61690d76','Los Angeles','Tigers','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('da6d3453-70b9-487d-b31e-f0f3d75fe005','e00927cb-a616-43d3-a82e-21439469ef13','Phoenix','Wolves','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('dab3e709-5dfb-4d40-b5e7-3fe17ff2b008','be7fb570-8794-4f13-94c2-8b9e2d8b137d','San Diego','Falcons','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('e1a2b3d4-f5b6-4f3d-a8e6-9b0c1d2e3014','0a95b9b1-6fb7-42a7-8333-56e649a48fe7','San Francisco','Wildcats','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('e5d5e6a2-e5e4-4d79-a3c5-ccfd36f7c012','acd5a6b8-89c5-40a9-87a5-2a097e02b9ed','Jacksonville','Jaguars','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('ee57d1c3-b41b-4be8-b45e-14f2a25b1001','2b1e02fc-4b92-4b0d-84a7-2418ff07ac13','New York','Lions','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000'),('f3c4e1a2-b3d5-46e8-b5c2-7f8d9e0a1015','e736c4f5-fc24-4be1-b7de-d8532d90cd2d','Columbus','Bulls','2024-03-06 14:00:00.000000','2024-03-06 14:00:00.000000');
/*!40000 ALTER TABLE `teams` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `FirstName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LastName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `HashedPassword` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Salt` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES ('12345678-90ab-cdef-1234-567890abcdef','Martin','Martinsen','87612345','martin@gmail.com','$2a$11$7fuBNabTNj8uExz4Ibev/OXNmyeJZcsyFQALZyPr/xMQp/nXhOJX.','$2a$11$7fuBNabTNj8uExz4Ibev/O','2024-02-22 15:33:58.553269','2024-02-22 15:33:58.553269'),('20065784-cdb9-465a-a439-6a627c448787','Siri','Ali','87654321','siri@abc.no','$2a$11$lbgXdxbXutlVq3H63LzCCeA8M1rUBLFH1J2OmSsrLTyWqlkTC4BAo','$2a$11$lbgXdxbXutlVq3H63LzCCi','2024-02-22 15:34:57.668488','2024-02-22 15:34:57.668488'),('20065784-cdb9-465a-a439-6a627c448ca8','Sara','Andersen','87654321','sara@abc.no','$2a$11$lbgXdxbXutlVq3H63LzCCeA8M1rUBLFH1J2OmSsrLTyWqlkTC4BAq','$2a$11$lbgXdxbXutlVq3H63LzCCe','2024-02-22 15:34:57.668488','2024-02-22 15:34:57.668488'),('22222222-2222-2222-2222-222222222222','Maria','Mariah','99999999','mariah@yahoo.com','$2a$11$2nb9L2C0b8QLyU5xRdpqtu7/Qw89vF7aLl0yWQ/dnMZ8M2N/cDBhK','$2a$11$2nb9L2C0b8QLyU5xRdpqtu','2024-02-22 19:54:51.886969','2024-02-22 19:54:51.886969'),('27c2097f-2d6d-44b4-9ecf-1b96dab3ab3d','Emma','Nilsen','56785678','emma@hotmail.com','$2a$11$5D5cdLLjF3W7fJztqyzyVu9cOvAFk.vd/jPpBRCm17WjwRvQoBblG','$2a$11$5D5cdLLjF3W7fJztqyzyVu','2024-02-22 15:33:58.553269','2024-02-22 15:33:58.553269'),('2bf14738-6f12-4af0-8e1b-7d79789b993f','Kristine','Knutsen','89674321','kristine@hotmail.com','$2a$11$Q3vTJN/47.6mAbG//tufleI27JxxvrxPNa5kFXUdQOY5/lWuEyFOm','$2a$11$Q3vTJN/47.6mAbG//tufle','2024-02-22 19:54:51.886969','2024-02-22 19:54:51.886969'),('2e88d66f-1d63-4bc2-90b5-0700458748ef','Per','Pedersen','56789012','per@msn.no','$2a$11$VG7DteI3f6MP5GJKukj9U.WOo0weRBCrNlnRj90DQSyd3.EwKdplW','$2a$11$VG7DteI3f6MP5GJKukj9U.','2024-02-22 15:33:58.553269','2024-02-22 15:33:58.553269'),('36ab0d4f-d7e1-43a7-85cd-b487cf74f3b8','Anne','Andersen','76129345','anne@outlook.com','$2a$11$C5ThG2WOGFDzGMDvNkddfe.ABpXZMz3FCcDqC.C47B2H/a2JgJdSO','$2a$11$C5ThG2WOGFDzGMDvNkddfe','2024-02-22 15:33:58.553269','2024-02-22 15:33:58.553269'),('48a9d05a-8b21-46d8-8714-8aa73a46c4e5','Hans','Hansen','87651234','hans@yahoo.no','$2a$11$g4Xkb.vEFpROfsyVouwa/u6AvdAeLnyHokHfogN3NV7ZxChnnCLOu','$2a$11$g4Xkb.vEFpROfsyVouwa/u','2024-02-22 19:54:51.886969','2024-02-22 19:54:51.886969'),('5c053d1b-6c70-4d3b-a030-35286a978b7a','Trine','Petersen','78675645','trine@yahoo.no','$2a$11$n8CzNXLkIFb0x4URvyJyLe.9l9ApDKZzzgMP/FVb3YBN5RUwCgHL.','$2a$11$n8CzNXLkIFb0x4URvyJyLe','2024-02-22 19:54:51.886969','2024-02-22 19:54:51.886969'),('7e3189d5-d015-4703-ad3f-9e6f2b7f2bdc','string','string','string','string','$2a$11$IggBvHWa1CtktzmOVrzyeuVMeQNc0couF0Mv9IA9Kc8eUGoGYuPkS','$2a$11$IggBvHWa1CtktzmOVrzyeu','2024-03-15 11:42:16.148812','2024-03-15 11:42:16.148812'),('820504a9-01cd-4812-b915-495e26fe0fa1','Ida','Olsen','23456789','ida@hotmail.com','$2a$11$GUbkt7FjG/pGEssbXOhxF.fgIMYwuyXzjKDeJW3FLaP2XZJ41/f/S','$2a$11$GUbkt7FjG/pGEssbXOhxF.','2024-02-22 19:54:51.886969','2024-02-22 19:54:51.886969'),('9d3de077-bb47-42ff-b695-170e6d08743a','Kari','Kristiansen','98761234','kari@gmail.com','$2a$11$yXApWYI1z.KlD/Z.9pF0TuAMauSBw8iGLoFqJXo81qiq4D24DGCl6','$2a$11$yXApWYI1z.KlD/Z.9pF0Tu','2024-02-22 15:33:58.553269','2024-02-22 15:33:58.553269'),('9ff9eab0-57ef-4530-ad29-1ebee9f682c3','Elsa','Rønningen','65436544','elsrøn@yyoyo.no','$2a$11$xFlYtJWVUrDUwCJ5n0T7tejzowXS0Ug7xbG/ZHFLOYBnKgB/8OLQO','$2a$11$xFlYtJWVUrDUwCJ5n0T7te','2024-02-22 20:08:36.759317','2024-02-22 20:08:36.759317'),('9ff9eab0-57ef-4530-ad29-1ebee9f686a6','Eivind','Ho','65436544','eiho@yyoyo.no','$2a$11$xFlYtJWVUrDUwCJ5n0T7tejzowXS0Ug7xbG/ZHFLOYBnKgB/8OLQO','$2a$11$xFlYtJWVUrDUwCJ5n0T7te','2024-02-22 20:08:36.759317','2024-02-22 20:08:36.759317'),('d9e4e229-7738-4d26-822d-1b13fb1052c9','Lars','Hansen','67896789','lars@hotmail.com','$2a$11$4XixT6.ZlhN9pLV0Kp58yONqSgFmN0YG6yolXzLccLws6yOgC9N5S','$2a$11$4XixT6.ZlhN9pLV0Kp58yO','2024-02-22 19:54:51.886969','2024-02-22 19:54:51.886969'),('eb7f4ad3-132d-4345-b5f3-b41762fe61f9','Ingrid','Johansen','32103210','ingrid@gmail.com','$2a$11$yvhdvrXH5pZwr/K05r1bf.h/RzpbGYZfKHs9DlTHPh.xbnMv9ADHe','$2a$11$yvhdvrXH5pZwr/K05r1bf.','2024-02-22 15:33:58.553269','2024-02-22 15:33:58.553269'),('f15a1513-eb40-4ca3-b8bb-c06959e1d009','Jølle','Walin','12345678','jwal@gmail.com','$2a$11$2evalcmM7WpglWzQEqC1eOi/kF9C6bZJjp9Td4QW5l2uu7ewFYEKq','$2a$11$2evalcmM7WpglWzQEqC1eO','2024-02-22 15:33:58.553269','2024-02-22 15:33:58.553269'),('f15a1513-eb40-4ca3-b8bb-c06959e1d6b5','Jens','Jensen','12345678','jens@gmail.com','$2a$11$2evalcmM7WpglWzQEqC1eOi/kF9C6bZJjp9Td4QW5l2uu7ewFYEKq','$2a$11$2evalcmM7WpglWzQEqC1eO','2024-02-22 15:33:58.553269','2024-02-22 15:33:58.553269'),('fdaca55a-2ed0-4760-b1ea-ead99086d1d7','Ole','Olsen','12312345','olsen@gmail.com','$2a$11$58nr7sj5oYCXV0dUpryxGeJEKU1tJ2LsO9MfIp9jD3b9o36nhW2se','$2a$11$58nr7sj5oYCXV0dUpryxGe','2024-02-22 19:54:51.886969','2024-02-22 19:54:51.886969'),('fdaca55a-2ed0-4760-b1ea-ead99086dccc','Salat','Amir','12312345','salami@gmail.com','$2a$11$58nr7sj5oYCXV0dUpryxGeJEKU1tJ2LsO9MfIp9jD3b9o36nhW2se','$2a$11$58nr7sj5oYCXV0dUpryxGe','2024-02-22 19:54:51.886969','2024-02-22 19:54:51.886969');
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

-- Dump completed on 2024-03-18 10:53:42

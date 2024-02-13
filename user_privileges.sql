DROP DATABASE IF EXISTS coach_connect;
CREATE DATABASE coach_connect;
USE coach_connect;


# create user
CREATE USER IF NOT EXISTS 'coach-app'@'localhost' IDENTIFIED BY 'coach123';
CREATE USER IF NOT EXISTS 'coach-app'@'%' IDENTIFIED BY 'coach123';
 
 # tilgang og rettigheter gis her:
GRANT ALL privileges ON coach_connect.* TO 'coach-app'@'%';
GRANT ALL privileges ON coach_connect.* TO 'coach-app'@'localhost';
FLUSH PRIVILEGES
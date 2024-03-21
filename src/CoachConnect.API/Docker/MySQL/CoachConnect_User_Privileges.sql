CREATE USER IF NOT EXISTS 'coach-app'@'localhost' IDENTIFIED BY 'coach123';
CREATE USER IF NOT EXISTS 'coach-app'@'%' IDENTIFIED BY 'coach123';
 
GRANT ALL privileges ON coach_connect.* TO 'coach-app'@'%';
GRANT ALL privileges ON coach_connect.* TO 'coach-app'@'localhost';
FLUSH PRIVILEGES;
CREATE TABLE `users`(
    `id` INT(10) AUTO_INCREMENT,
    `Login` varchar(50) UNIQUE NOT NULL,
    `AccessKey` varchar(50) NOT NULL,
    PRIMARY KEY (`ID`)
)
COLLATE='latin1_swedish_ci' 
ENGINE=InnoDB;
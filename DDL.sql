CREATE DATABASE `neo_lethal_blast`;
USE `neo_lethal_blast`;
CREATE TABLE `word_datas` (
  `id` INT NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `type` ENUM('SMASH', 'SLASH', 'PENE') NOT NULL,
  `element` ENUM('FIRE', 'WATER', 'WIND') NOT NULL,
  `not_use` TINYINT NULL,
  PRIMARY KEY (`id`));
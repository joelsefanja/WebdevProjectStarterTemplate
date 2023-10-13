use nhlstendencafe;
drop table if exists users;

CREATE TABLE Users (
                       Id INT AUTO_INCREMENT PRIMARY KEY,
                       Email VARCHAR(255) NOT NULL,
                       PasswordHash VARCHAR(255) NOT NULL,
                       FirstName VARCHAR(255),
                       LastName VARCHAR(255),
                       CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

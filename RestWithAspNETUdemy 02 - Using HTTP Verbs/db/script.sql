-- use `rest_with_asp_net_udemy`;

CREATE TABLE `persons`
(
	`Id` int
(10) unsigned default null,
	`FirstName` varchar
(50) null default null,
	`LastName` varchar
(50) null default null,
	`Address` varchar
(50) null default null,
	`Gender` varchar
(50) null default null
)
ENGINE=InnoDB
;

alter table `persons` change Id Id INT
(10) auto_increment primary key
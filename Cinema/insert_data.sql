INSERT INTO `genres` VALUES 
(1, 'Action'),
(2, 'Adventure'),
(3, 'Horror'),
(4, 'Romantic'),
(5, 'Comedy'),
(6, 'Fantasy');

-- (`id`, `movie_name`, `imdb_link`, `premier_year`, `cover_image`) 
INSERT INTO `movies` VALUES 
('1', 'The Batman', 'https://www.imdb.com/title/tt1877830/', '2022', 'https://firebasestorage.googleapis.com/v0/b/winformscinema.appspot.com/o/the_batman_cover.png?alt=media&token=2bae014f-efd4-4740-a878-a0efa716d75d'),
('2', 'CODA', 'https://www.imdb.com/title/tt10366460/', '2021', 'https://firebasestorage.googleapis.com/v0/b/winformscinema.appspot.com/o/CODA_cover.png?alt=media&token=4012fbe3-465c-4973-8805-445950f8304a'),
('3', 'Death on the Nile', 'https://www.imdb.com/title/tt7657566/', '2022', 'https://firebasestorage.googleapis.com/v0/b/winformscinema.appspot.com/o/death_on_the_nile.png?alt=media&token=3b91b052-f305-43ff-8c6a-5da5efbeb8cd');

INSERT INTO `winforms-cinema`.`movie_genres` (`MovieId`, `GenreId`) VALUES ('1', '2'),
('1', '4'),
('2', '1'),
('2', '3'), 
('2', '6'),
('3', '5'),
('3', '6'),
('3', '1');

INSERT INTO `scenes` VALUES
(1, "SCENE1", '{
   "scene":{
      "1":{
         "1":"0",
         "2":"0",
         "3":"0",
         "4":"0",
         "5":"0"
      },
      "2":{
         "1":0,
         "2":0,
         "3":0,
         "4":0,
         "5":0
      },
      "3":{
         "1":0,
         "2":0,
         "3":0,
         "4":0,
         "5":0
      }
   }
}'),
(2, "SCENE2", '{
   "scene":{
      "1":{
         "1":"1",
         "2":"1",
         "3":"1",
         "4":"1",
         "5":"1"
      },
      "2":{
         "1":0,
         "2":0,
         "3":0,
         "4":0,
         "5":0
      },
      "3":{
         "1":0,
         "2":0,
         "3":0,
         "4":0,
         "5":0
      }
   }
}');

INSERT INTO `projections` VALUES
(1, 1, 1, 25, null, '2022/08/04 18:10'),
(2, 2, 2, 20, null, '2022/10/06 16:00'),
(3, 1, 1, 10, null, '2022/10/06 16:00'),
(4, 3, 2, 15, null, '2022/10/06 19:00');

INSERT INTO `tickets` VALUES
('1a5565a0-b71c-11ec-b909-0242ac120002', 1, 11, 'Pesho Peshov', '2022/08/04 12:16', '2022/08/04 12:16'),
('bc9e71e4-b71c-11ec-b909-0242ac120002', 2, 11, 'Georgi Georgiev', '2022/08/04 12:17', '2022/08/04 12:17');
































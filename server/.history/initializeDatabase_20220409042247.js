import {
    DataTypes,
    Sequelize
} from 'sequelize';
import {
    Movies
} from './model/movie.js';
import {
    Genres
} from './model/genre.js';
import {
    Tickets
} from './model/ticket.js';
import {
    Scenes
} from './model/scene.js';
import {
    Projections
} from './model/projection.js';
import {
    MovieGenres
} from './model/MovieGenres.js';

let Movie
let Genre
let Ticket
let Scene
let Projection
let MovieGenre

export const sequelize = new Sequelize('mysql://root:meuhas12emili@localhost:3306/winforms-cinema') // mysql connection

//test connection
try {
    await sequelize.authenticate();
    console.log('Connection has been established successfully.');
} catch (error) {
    console.error('Unable to connect to the database:', error);
}

//TO DO: Add validations
Movie = Movies(sequelize, DataTypes)
Genre = Genres(sequelize, DataTypes)
Ticket = Tickets(sequelize, DataTypes)
Scene = Scenes(sequelize, DataTypes)
Projection = Projections(sequelize, DataTypes)
MovieGenre = MovieGenres(sequelize, DataTypes, Movie, Genre)

Movie.belongsToMany(Genre, {
    through: 'Movie_Genres',
});
Genre.belongsToMany(Movie, {
    through: 'Movie_Genres'
});

//Movie.belongsTo(Projection, {foreignKey: 'movie_id', constraints: false})
//Movie.belongsToMany(Projection, {through: 'movie_id', constraints: false})

Projection.hasOne(Movie, {thro: 'movie_id', constraints: false})

//Scene.belongsTo(Projection, {foreignKey: 'scene_id'})
//Projection.hasOne(Scene)

//Check if table is created, if it isn't, create it
await sequelize.sync();
console.log("All models were synchronized successfully.");


export let models = {
    Movie: Movie,
    Genre: Genre,
    Ticket: Ticket,
    Scene: Scene,
    Projection: Projection,
    MovieGenre: MovieGenre
}

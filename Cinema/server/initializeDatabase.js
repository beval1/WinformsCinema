import {
    DataTypes,
    Sequelize
} from 'sequelize';
import {
    Movies
} from './model/Movie.js';
import {
    Genres
} from './model/Genre.js';
import {
    Tickets
} from './model/Ticket.js';
import {
    Scenes
} from './model/Scene.js';
import {
    Projections
} from './model/Projection.js';
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

Projection.belongsTo(Movie, {foreignKey: 'movie_id'})
Projection.belongsTo(Scene, {foreignKey: 'scene_id'})

Ticket.belongsTo(Projection, {foreignKey: 'projection_id'})


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

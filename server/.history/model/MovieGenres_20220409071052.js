export const MovieGenres = (sequelize, DataTypes, Movie, Genre) => sequelize.define('Movie_Genres', {
    MovieId: {
      type: DataTypes.INTEGER,
      // references: {
      //   model: Movie, // 'Movies' would also work
      //   key: 'id'
      // }
    },
    GenreId: {
      type: DataTypes.INTEGER,
      // references: {
      //   model: Genre, // 'Genres' would also work
      //   key: 'id'
      // }
    }
}, {timestamps: false, });
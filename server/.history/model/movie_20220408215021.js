export const Movies = (sequelize, DataTypes) => sequelize.define('Movie', {
    // Model attributes are defined here
    movieName: {
        type: DataTypes.STRING,
        allowNull: false,
        defaultValue: "UNKNOWN",
    },
    imdbLink: {
        type: DataTypes.STRING,
        allowNull: true,
        defaultValue: "UNKNOWN",
    },
    premierYear: {
        type: DataTypes.STRING,
        allowNull: true,
        defaultValue: "UNKNOWN",
    },
    coverImage: {
        type: DataTypes.STRING,
        allowNull: true,
        defaultValue: "UNKNOWN",
    },
}, {
    // Other model options go here
    tableName: 'movies',
    timestamps: false,
    underscored: true,
});

//module.exports.Movies = Movies;
export const Genres = (sequelize, DataTypes) => sequelize.define('Genre', {
    // Model attributes are defined here
    genreName: {
        type: DataTypes.STRING,
        allowNull: false,
    },
}, {
    // Other model options go here
    tableName: 'genres',
    timestamps: false,
    underscored: true,
});


//module.exports.Genres = Genres;
export const Projections = (sequelize, DataTypes) => sequelize.define('Projection', {
    // Model attributes are defined here
    movieId: {
        type: DataTypes.INTEGER,
        allowNull: false,
        // references: {
        //     model: "movies",
        //     key: "id"
        // },
    },
    sceneId: {
        type: DataTypes.INTEGER,
        allowNull: false,
        // references: {
        //     model: "scenes",
        //     key: "id"
        // },
    },
    ticketPrice: {
        type: DataTypes.D,
        allowNull: false,
    },
    sceneSeats: {
        type: DataTypes.JSON,
        allowNull: true,
    },
    projectionTime: {
        type: DataTypes.DATE,
        allowNull: false,
    },
}, {
    // Other model options go here
    tableName: 'projections',
    timestamps: false,
    underscored: true,
});
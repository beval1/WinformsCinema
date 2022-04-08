export const Scenes = (sequelize, DataTypes) => sequelize.define('Scene', {
    // Model attributes are defined here
    name: {
        type: DataTypes.STRING,
        allowNull: false,
    },
    sceneSeats: {
        type: DataTypes.JSON,
        allowNull: false,
    },
}, {
    // Other model options go here
    tableName: 'scenes',
    timestamps: false,
    underscored: true,
});

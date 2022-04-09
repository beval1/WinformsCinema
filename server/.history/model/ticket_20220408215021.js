export const Tickets = (sequelize, DataTypes) => sequelize.define('Ticket', {
    // Model attributes are defined here
    uuid: {
        type: DataTypes.UUID,
        defaultValue: DataTypes.UUIDV1,
        primaryKey: true
    },
    projectionId: {
        type: DataTypes.INTEGER,
        allowNull: false,
        references: {
            model: "projections",
            key: "id"
        },
    },
    seatId: {
        type: DataTypes.INTEGER,
        allowNull: false,
    },
    ownerFullName: {
        type: DataTypes.STRING,
        allowNull: false,
    },
}, {
    // Other model options go here
    tableName: 'tickets',
    //timestamps: false,
    underscored: true,
});
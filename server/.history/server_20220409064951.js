import Express from 'express';
import cors from 'cors';
import {
    models
} from './initializeDatabase.js'

const app = Express();
const port = 3002;
//const portHttp = 3003;
//const portHttps = 3002;
app.use(Express.urlencoded({
    extended: true
}));
app.use(Express.json());


//var httpServer = http.createServer(app);
//var httpsServer = https.createServer(credentials, app);

//cors bypass
app.use(cors())

app.get("/get-movies", async (req, res) => {
    let offset = req.query.offset ? Number(req.query.offset) : 0;
    let limit = req.query.limit ? Number(req.query.limit) : 1;
    console.log(`Limit: ${limit}`)
    console.log(`Offset: ${offset}`)
    let result = await models.Movie.findAll({
        offset: offset,
        limit: limit,
        raw: true
    });

    console.log(result);
    //send data to the client;
    res.json({
        data: result
    })
})

app.get("/get-projections", async (req, res) => {
    let offset = req.query.offset ? Number(req.query.offset) : 0;
    let limit = req.query.limit ? Number(req.query.limit) : null;
    console.log(`Limit: ${limit}`)
    console.log(`Offset: ${offset}`)
    let result = await models.Projection.findAll({
        attributes: {
            exclude: ['movie_id', 'scene_id']
        },
        include: [{
            model: models.Movie,
            required: true,
            include: [{
                model: models.Genre,
                required: true,
                attributes: ['genre_name']
            }]
        }, 
        {
            model: models.Scene,
            required: true
        },
    ],
        offset: offset,
        limit: limit,
    });

    console.log(result[0].Movie.Genres.dataValues.genre_name);
    let genresArr = []
    result[0].Movie.Genres.data.forEach(g => genresArr.push(g.genre_name))
    result.Movie.Genres.Genres = genresARr;

    console.log(result);
    //send data to the client;
    res.json({
        data: result
    })
})

app.post("/add-movie", async (req, res) => {
    //get data from request body;
    const data = req.body;
    console.log(data);
    //store in database using ORM;
    let movie = {
        movieName: data.movieName,
        premierYear: data.premierYear,
        coverImage: data.coverImage,
        imdbLink: data.imdbLink,
    }
    const savedMovie = await models.Movie.create(movie);
    data.genres.forEach(async genre => {
        console.log(genre)
        console.log(savedMovie)
        await models.MovieGenre.create({
            movieId: savedMovie._prevousDataValues.id,
            genreId: Number(genre)
        })
    });

    res.json({
        message: "saved successfuly!"
    })
})

// app.get("/get-location", async (req, res) => {
//     console.log(req.query.search)

//     const result = await sequelize.query("SELECT * FROM locations WHERE street_address LIKE :search", {
//         model: Location,
//         mapToModel: true,
//         type: sequelize.QueryTypes.SELECT,
//         replacements: {
//             search: `%${req.query.search}%`,
//         },
//     });
//     console.log(result);
//     //send data to the client;
//     res.json({
//         data: result
//     })
// })

// app.get("/get-countries", async (req, res) => {
//     const results = await sequelize.query("SELECT DISTINCT country FROM locations ORDER BY country ASC", {
//         raw: true,
//         type: sequelize.QueryTypes.SELECT
//     });
//     res.json({
//         data: results
//     })
// });

// app.get("/get-cities", async (req, res) => {
//     const results = await sequelize.query("SELECT DISTINCT city FROM locations ORDER BY city ASC", {
//         raw: true,
//         type: sequelize.QueryTypes.SELECT
//     });
//     res.json({
//         data: results
//     })
// });

// app.get("/get-addresses", async (req, res) => {
//     console.log(req.query.countries)
//     console.log(req.query.cities)

//     let countries = [];
//     let cities = [];

//     if (req.query.countries.includes('All') || req.query.countries == 0) {
//         const result = await sequelize.query("SELECT DISTINCT country FROM locations ORDER BY city ASC", {
//             raw: true,
//             type: sequelize.QueryTypes.SELECT
//         });
//         result.forEach(r => {
//             countries.push(r.country)
//         })
//     } else {
//         countries = Array.from(req.query.countries.split(','))
//     }
//     if (req.query.cities.includes('All') || req.query.cities == 0) {
//         const result = await sequelize.query("SELECT DISTINCT city FROM locations ORDER BY city ASC", {
//             raw: true,
//             type: sequelize.QueryTypes.SELECT
//         });
//         result.forEach(r => {
//             cities.push(r.city)
//         })
//     } else {
//         cities = Array.from(req.query.cities.split(','))
//     }

//     console.log(countries)
//     console.log(cities)

//     let sqlQuery = "SELECT DISTINCT street_address FROM locations WHERE country IN(:countries) AND city IN(:cities) ORDER BY street_address ASC"
//     const results = await sequelize.query(sqlQuery, {
//         raw: true,
//         type: sequelize.QueryTypes.SELECT,
//         replacements: {
//             countries: countries,
//             cities: cities,
//         },
//     });
//     res.json({
//         data: results
//     })
// });

// app.post("/add-location", async (req, res) => {
//     //get data from request body;
//     const data = req.body;
//     console.log(data);
//     //store in database using ORM;
//     const location = await Location.create(data);

//     res.json({
//         message: "saved successfuly!"
//     })
// })


//httpServer.listen(portHttp);
//httpsServer.listen(portHttps)
app.listen(port, () => console.log("listening on port: " + port))
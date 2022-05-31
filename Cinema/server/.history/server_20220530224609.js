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

app.post("/create-ticket", async (req, res) => {
    //get data from request body;
    const projection = req.body;
    console.log(projection);
    console.log(projection.sceneSeats.sceneSeats);

    // first update projection scene
    models.Projection.update(
        {sceneSeats: projection.sceneSeats.sceneSeats},
        { where: { id: projection.id } }
    ).success(result => {

    }).error(result => {
        
    })

    //store in database using ORM;
    // let movie = {
    //     movieName: data.movieName,
    //     premierYear: data.premierYear,
    //     coverImage: data.coverImage,
    //     imdbLink: data.imdbLink,
    // }
    // const savedMovie = await models.Movie.create(movie);
    // data.genres.forEach(async genre => {
    //     console.log(genre)
    //     console.log(savedMovie)
    //     await models.MovieGenre.create({
    //         movieId: savedMovie._prevousDataValues.id,
    //         genreId: Number(genre)
    //     })
    // });

    res.json({
        message: "saved successfuly!"
    })
})

//httpServer.listen(portHttp);
//httpsServer.listen(portHttps)
app.listen(port, () => console.log("listening on port: " + port))
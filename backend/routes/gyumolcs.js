import express from 'express';
import connection from '../db.js';
const gyumolcsRouter = express.Router();

gyumolcsRouter.use(express.json());
gyumolcsRouter.use(express.urlencoded({extended: true}));

async function getGyumolcsok(){
    try{
        const [rows] = await connection.query('SELECT * FROM gyumolcs'); 
        return rows; 
    } catch (error) {
    console.error('Hiba a gyümölcsök lekérésekor:', error); throw error; } 
} 

async function getGyumolcsById(id) {
    try {
        const [rows] = await connection.query('SELECT * FROM gyumolcs WHERE gyumolcsid = ?', [id]);
        return rows;
    } catch (error) {
        console.error('Hiba a gyümölcs lekérésekor:', error);
        throw error;
    }
}

gyumolcsRouter.get('/', async (req, res) => {
    try {
        const [rows] = await connection.query('SELECT * FROM gyumolcs');
        res.json(rows);
    } catch (error) {
        console.error("TELJES HIBA:", error);
        res.status(500).json({
            message: error.message,
            code: error.code,
            errno: error.errno,
            sqlMessage: error.sqlMessage
        });
    }
});




gyumolcsRouter.get('/:id', async (req, res) => {
getGyumolcsById(req.params.id).then(data => res.json(data)).catch(error => res.status(500).json({ error: 'Hiba a gyümölcs lekérésekor' }));
});

gyumolcsRouter.post('/', async (req, res) => {
    try{
        const { nev, megjegyzes, nev_eng, alt_szoveg, src } = req.body;
        if (!nev || !megjegyzes || !nev_eng || !alt_szoveg || !src) {
            throw new Error('Hiányzó adat');
        }
        const sql = "INSERT INTO `gyumolcs` (`gyumolcsid`, `nev`, `megjegyzes`, `nev_eng`, `alt_szoveg`, `src`) VALUES (NULL, ?, ?, ?, ?, ?);";
        const [result] = await connection.query(sql, [nev, megjegyzes, nev_eng, alt_szoveg, src]);
        res.json({ id: result.insertId, nev, megjegyzes, nev_eng, alt_szoveg, src });
    } catch (error) {
        console.error('Hiba a gyümölcs hozzáadásakor:', error);
        res.status(500).json({ error: 'Hiba a gyümölcs hozzáadásakor' });
    }
}); 


gyumolcsRouter.put('/:id', async (req, res) => {
    try {
        const { nev, megjegyzes, nev_eng, alt_szoveg, src } = req.body;
        if (!req.params.id || !nev || !megjegyzes || !nev_eng || !alt_szoveg || !src) {
            return res.status(400).json({ error: 'Hiányzó adat(ok)' });
        }

        const sql = "UPDATE `gyumolcs` SET `nev`= ?, `megjegyzes`= ?, `nev_eng`= ?, `alt_szoveg`= ?, `src`= ? WHERE `gyumolcsid`= ?";
        await connection.query(sql, [nev, megjegyzes, nev_eng, alt_szoveg, src, req.params.id]);

        res.status(200).json({ message: "Gyümölcs módosítva", id: req.params.id });
    } catch (error) {
        console.error('Hiba a gyümölcs módosításakor:', error);
        res.status(500).json({ error: 'Hiba a gyümölcs módosításakor' });
    }
});

gyumolcsRouter.delete('/:id', async (req, res) => {
    try {
        if (!req.params.id) return res.status(400).json({ error: 'Hiányzó adat' });

        const sql = "DELETE FROM `gyumolcs` WHERE `gyumolcsid`= ?";
        await connection.query(sql, [req.params.id]);

        res.status(200).json({ message: "Gyümölcs törölve", id: req.params.id });
    } catch (error) {
        console.error('Hiba a gyümölcs törlésekor:', error);
        res.status(500).json({ error: 'Hiba a gyümölcs törlésekor' });
    }
});



export default gyumolcsRouter;
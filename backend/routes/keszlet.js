import express from 'express';
import connection from '../db.js';
const keszletRouter = express.Router();

keszletRouter.get('/', async (req, res) => {
    const sql = "SELECT * FROM keszlet";
    try{ 
        const [rows] = await connection.query(sql); res.json(rows); 
    }catch (error) { 
        console.error('Hiba a készlet lekérésekor:', error); res.status(500).json({ error: 'Hiba a készlet lekérésekor' }); 
    }
});

keszletRouter.post('/', async (req, res) => {
    try {
        const { gyumolcsid, mennyiseg, egysegar } = req.body;
        if (!gyumolcsid || !mennyiseg || !egysegar) {
            return res.status(400).json({ error: 'Hiányzó adat' });
        }

        const sql = "INSERT INTO `keszlet` (`gyumolcsid`, `mennyiseg`, `egysegar`) VALUES (?, ?, ?)";
        const [result] = await connection.query(sql, [gyumolcsid, mennyiseg, egysegar]);

        res.status(201).json({ message: "Készlet hozzáadva", id: result.insertId });
    } catch (error) {
        console.error('Hiba a készlet hozzáadásakor:', error);
        res.status(500).json({ error: 'Hiba a készlet hozzáadásakor' });
    }
});


keszletRouter.put('/:id', async (req, res) => {
    try {
        const { gyumolcsid, mennyiseg, egysegar } = req.body;
        if (!req.params.id || !gyumolcsid || !mennyiseg || !egysegar) {
            return res.status(400).json({ error: 'Hiányzó adat(ok)' });
        }

        const sql = "UPDATE `keszlet` SET `gyumolcsid`= ?, `mennyiseg`= ?, `egysegar`= ? WHERE `keszletid`= ?";
        await connection.query(sql, [gyumolcsid, mennyiseg, egysegar, req.params.id]);

        res.status(200).json({ message: "Készlet módosítva", id: req.params.id });
    } catch (error) {
        console.error('Hiba a készlet módosításakor:', error);
        res.status(500).json({ error: 'Hiba a készlet módosításakor' });
    }
});


export default keszletRouter;
import mysql2 from 'mysql2/promise';

const connection = mysql2.createPool({
    host: 'localhost',
    user: 'root',
    password: '', 
    database: 'gyumolcsok',
    port: 3307,
});

export default connection; 
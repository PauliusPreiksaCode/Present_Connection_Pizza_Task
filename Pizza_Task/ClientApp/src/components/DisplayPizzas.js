import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

const DisplayPizzas = () => {

    const [items, setItems] = useState([]);

    useEffect(() => {
        fetch("https://localhost:7265/api/pizza-task/get-pizzas")
            .then((results) => {
                return results.json();
            })
            .then((data) => {
                setItems(data);
            }).
            catch((error) => {
                const items = null;
                setItems(items);
            })
    }, []);

    return (

        <div className="container">
            <h1>Orders list</h1>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>Pizza size</th>
                        <th>Price</th>
                        <th>Toppings</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        (items != null) ? items.map((item, index) => (
                            <tr key={index}>
                                <td>{item.basePizza.sizes}</td>
                                <td>{item.basePizza.price.toFixed(2)}</td>
                                <td>{item.toppings.join(", ")}</td>
                            </tr>
                        )) : <tr><td colSpan="3" style={{ textAlign: "center" }}>You have no orders</td></tr>}
                </tbody>
            </table>
        </div>
    );
}

export default DisplayPizzas;

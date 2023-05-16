import React, { useState } from 'react';

const toppingsList = [
    { id: 1, name: 'Cheese' },
    { id: 2, name: 'Pepperoni' },
    { id: 3, name: 'Mushrooms' },
    { id: 4, name: 'Sausage' },
    { id: 5, name: 'Onions' },
    { id: 6, name: 'Olives' },
    { id: 7, name: 'Ham' },
    { id: 8, name: 'Bacon' },
    { id: 9, name: 'Jalapenos' },
];

const PizzaOrderForm = () => {
    const [size, setSize] = useState('Small');
    const [toppings, setToppings] = useState([]);
    const [pizzaOrderPreview, setPizzaOrderPreview] = useState(null);
    const [pizzaOrderPlaced, setPizzaOrderPlaced] = useState(null);

    const handleSizeChange = (e) => {
        setSize(e.target.value);
    };

    const handleToppingChange = (e) => {
        const toppingId = parseInt(e.target.value);
        const isChecked = e.target.checked;

        if (isChecked) {
            setToppings([...toppings, toppingId]);
        } else {
            setToppings(toppings.filter((id) => id !== toppingId));
        }
    };
    //44418

    const handleSubmit = (e) => {
        e.preventDefault();
        fetch('https://localhost:7265/api/pizza-task/calculate-cost', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                basePizza: {
                    sizes: size,
                },
                toppings: toppings.map((id) =>
                    toppingsList.find((topping) => topping.id === id).name
                ),
            }),
        })
            .then((response) => response.json())
            .then((data) => {
                const pizzaOrderPreview = `Size: ${size}, Toppings: ${toppings
                    .map((id) => toppingsList.find((topping) => topping.id === id).name)
                    .join(', ')}, Cost: ${data.toFixed(2)}`;
                setPizzaOrderPreview(pizzaOrderPreview);
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    };

    const handlePlaceOrder = () => {
        fetch('https://localhost:7265/api/pizza-task/add-pizza', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                basePizza: {
                    sizes: size,
                },
                toppings: toppings.map((id) =>
                    toppingsList.find((topping) => topping.id === id).name
                ),
            }),
        })
            .then((response) => response.json())
            .then((data) => {
                const pizzaOrderPlaced = 'Order placed successfully!';
                setPizzaOrderPlaced(pizzaOrderPlaced);
            })
            .catch((error) => {
                console.error(error);
            });
    };

    return (
        <form onSubmit={handleSubmit}>
            <div className="input-box">
                <h1>Order a pizza</h1>
                <label htmlFor="size">Select a size:</label>
                <select
                    id="size"
                    name="size"
                    value={size}
                    onChange={handleSizeChange}
                    className="form-control"
                >
                    <option value="Small">Small</option>
                    <option value="Medium">Medium</option>
                    <option value="Large">Large</option>
                </select>
            </div>
            <div className="input-box">
                <label className="top-label">Select toppings:</label>
                {toppingsList.map((topping) => (
                    <div key={topping.id} className="form-check">
                        <input
                            type="checkbox"
                            id={`topping-${topping.id}`}
                            name="toppings"
                            value={topping.id}
                            checked={toppings.includes(topping.id)}
                            onChange={handleToppingChange}
                            className="form-check-input"
                        />
                        <label htmlFor={`topping-${topping.id}`} className="form-check-label">
                            {topping.name}
                        </label>
                    </div>
                ))}
            </div>
            <button type="submit" className="btn btn-primary">
                Preview Order
            </button>
            <div>
                {pizzaOrderPreview && (
                    <div>
                        <h2>Pizza Order Preview</h2>
                        <p>{pizzaOrderPreview}</p>
                        <button onClick={handlePlaceOrder} className="btn btn-primary">
                            Place Order
                        </button>
                        {pizzaOrderPlaced && (
                            <h2>{pizzaOrderPlaced}</h2>
                        )}
                    </div>
                )}
            </div>
        </form>
    );
};

export default PizzaOrderForm;


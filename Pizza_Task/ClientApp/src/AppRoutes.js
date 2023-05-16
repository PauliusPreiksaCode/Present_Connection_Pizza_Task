import DisplayPizzas from './components/DisplayPizzas';
import PizzaOrderForm from './components/PizzaOrderForm';


const AppRoutes = [
  {
    index: true,
        element: <PizzaOrderForm />
  },
  {
    path: '/display-pizzas',
    element: <DisplayPizzas />
  },
  {
    path: '/order-pizza',
    element: <PizzaOrderForm />
  }

];

export default AppRoutes;

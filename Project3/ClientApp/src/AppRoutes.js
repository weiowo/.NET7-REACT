import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import SignUp from "./components/SignUp"
import Restaurant from "./components/Restaurant"


const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
    },
    {
        path: '/signup',
        element: <SignUp />
    },
    {
        path: '/restaurant-system',
        element: <Restaurant />
    }

];

export default AppRoutes;

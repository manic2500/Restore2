/* import { useEffect, useState } from 'react'
import type { Product } from './models/product';
import Catalog from '@/features/catalog/Catalog';
import Header from './layout/header';
import Footer from './layout/footer';
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import RootLayout from './layout/RootLayout';
//import Home from '@/features/home/Home';


function App() {
  const [products, setProducts] = useState<Product[]>();

  useEffect(() => {

    fetch('https://localhost:5001/api/products')
      .then(response => response.json())
      .then(data => setProducts(data))

    return () => { }

  }, [])

  return (
    <>
      <Router>
        <div className="flex h-screen flex-col">
          <Header />
          <main className="flex-1 wrapper">

            <Routes>
              <Route path='/' element={<RootLayout />}>
                {<Route index path='/' element={<Home />} />}
                <Route index path='/' element={<Catalog products={products} />} />
              </Route>
            </Routes>

            
          </main>
          <Footer />
        </div>
      </Router>
    </>
  )
}

export default App*/


/* 

<Container maxWidth='xl'>
      <Typography variant='h4'>Re-Store</Typography>
      <Catalog products={products} />
    </Container>

*/ 
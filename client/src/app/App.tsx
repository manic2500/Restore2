import Header from './layout/header';
import Footer from './layout/footer';
import { Outlet } from "react-router-dom";

function App() {
  return (
    <>
      <div className="flex h-screen flex-col">
        <Header />
        <main className="flex-1 wrapper">
          <Outlet />
        </main>
        <Footer />
      </div>
    </>
  )
}

export default App


/* 

<Container maxWidth='xl'>
      <Typography variant='h4'>Re-Store</Typography>
      <Catalog products={products} />
    </Container>

*/
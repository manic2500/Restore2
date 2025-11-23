import Header from './layout/header';
import Footer from './layout/footer';
import { Outlet } from "react-router-dom";
import { useAppSelector } from './store/hooks';
import LoadingSpinner from '@/components/loadingSpinner';

function App() {
  const { isLoading } = useAppSelector(state => state.ui);

  return (
    <>
      {/* Loader overlay */}
      {isLoading && (
        <div className="fixed inset-0 z-50 flex items-center justify-center">
          <LoadingSpinner />
        </div>
      )}

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
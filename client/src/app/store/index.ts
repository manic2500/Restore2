import { configureStore } from "@reduxjs/toolkit";
import { counterSlice } from "./slices/counterSlice";
import { catalogApi } from "@/features/catalog/catalogApi";
import { uiSlice } from "./slices/uiSlice";
import { errorApi } from "@/features/about/errorApi";

// Store
const store = configureStore({
    reducer: {
        [catalogApi.reducerPath]: catalogApi.reducer,
        [errorApi.reducerPath]: errorApi.reducer,
        counter: counterSlice.reducer,
        ui: uiSlice.reducer
    },
    middleware: (getDefaultMiddlware) =>
        getDefaultMiddlware().concat(catalogApi.middleware, errorApi.middleware)
})
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export default store;
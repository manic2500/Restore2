import { createSlice, type PayloadAction } from "@reduxjs/toolkit";

export type CounterState = {
    data: number
}

const initialState: CounterState = {
    data: 0
}

// Slice
export const counterSlice = createSlice({
    name: 'counter',
    initialState: initialState,
    reducers: {
        increment: (state, action: PayloadAction<number | undefined>) => {
            state.data += action.payload ?? 1;
        },
        decrement: (state, action: PayloadAction<number | undefined>) => {
            state.data -= action.payload ?? 1;
        }
    }
})

// Actions
export const { increment, decrement } = counterSlice.actions

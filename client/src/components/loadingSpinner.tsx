import loader from "../assets/loader.gif"; // adjust path as needed

export default function LoadingSpinner() {
    return (
        <div className="flex justify-center items-center h-screen w-screen">
            <div className="relative h-[150px] w-[150px]">
                <img
                    src={loader}
                    alt="Loading..."
                    style={{
                        width: "100%",
                        height: "100%",
                        objectFit: "contain",
                    }}
                />
            </div>
        </div>
    );
}

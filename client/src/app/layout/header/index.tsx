import { APP_NAME } from "@/lib/constants";
import Menu from "./menu";
import { Link } from "react-router-dom";
import { Button } from "@/components/ui/button";

export default function Header() {
    return (
        <div className="w-full border-b sticky top-0 z-50 shadow-sm bg-neutral-900 text-white">
            <div className="wrapper flex-between">
                <div className="flex-start">
                    <Link to={'/'} className="flex-start">
                        <img src={'/images/logo.svg'} alt={`${APP_NAME}`} height={48} width={48} />
                        <span className="hidden lg:block font-bold text-2xl ml-3">{APP_NAME}</span>
                    </Link>
                </div>
                <div>
                    <Button asChild variant={'link'} style={{ fontSize: 20 }}>
                        <Link to={'/catalog'} className="text-white">
                            Catalog
                        </Link>
                    </Button>
                    <Button asChild variant={'link'} style={{ fontSize: 20 }}>
                        <Link to={'/about'} className="text-white">
                            About
                        </Link>
                    </Button>
                    <Button asChild variant={'link'} style={{ fontSize: 20 }}>
                        <Link to={'/contact'} className="text-white">
                            Contact
                        </Link>
                    </Button>
                </div>
                <Menu />
            </div>
        </div>
    );
};
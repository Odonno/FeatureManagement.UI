import { FunctionalComponent, h } from "preact";
import * as style from "./style.css";

const Header: FunctionalComponent = () => {
    return (
        <header class={style.header}>
            <h1>Features</h1>
        </header>
    );
};

export default Header;

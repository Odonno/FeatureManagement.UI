import { FunctionalComponent, h } from "preact";
import * as style from "./style.css";
import { useState, useEffect } from "preact/hooks";
import { Feature } from '../../models';
import { env } from '../../config';

const Home: FunctionalComponent = () => {
    const [features, setFeatures] = useState<Feature[]>([]);

    useEffect(() => {
        fetch(env.apiEndpoint)
            .then<Feature[]>(res => res.json())
            .then(features => {
                setFeatures(features);
            });
    }, []);

    return (
        <div class={style.home}>
            {features.map(f => {
                return (
                    <div>
                        <h1>{f.name}</h1>
                        {f.description &&
                            <p>{f.description}</p>
                        }
                    </div>
                );
            })}
        </div>
    );
};

export default Home;

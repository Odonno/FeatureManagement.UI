import { FunctionalComponent, h } from "preact";
import * as style from "./style.css";
import { useState, useEffect } from "preact/hooks";
import { Feature } from '../../models';
import { env } from '../../config';
import { Toggle } from '@fluentui/react/lib/Toggle';

const Home: FunctionalComponent = () => {
    const [features, setFeatures] = useState<Feature[]>([]);

    useEffect(() => {
        fetch(env.apiEndpoint)
            .then<Feature[]>(res => res.json())
            .then(features => {
                setFeatures(features);
            });
    }, []);

    const handleFeatureChange = (feature: Feature) => {
        const payload = {
            value: !feature.enabled
        };

        fetch(`${env.apiEndpoint}/${feature.name}/set`, {
            method: 'POST',
            body: JSON.stringify(payload)
        })
            .then<Feature>(res => res.json())
            .then(feature => {
                setFeatures(features => {
                    return features.map(f => {
                        if (f.name === feature.name) {
                            return feature;
                        }
                        return f;
                    });
                });
            });
    };

    return (
        <div class={style.home}>
            {features.map(f => {
                return (
                    <div>
                        <h1>{f.name}</h1>
                        {f.description &&
                            <p>{f.description}</p>
                        }
                        <Toggle
                            checked={f.enabled}
                            onChange={() => handleFeatureChange(f)}
                        />
                    </div>
                );
            })}
        </div>
    );
};

export default Home;

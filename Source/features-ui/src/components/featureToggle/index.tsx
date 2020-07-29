// @ts-nocheck
import { FunctionalComponent, h } from "preact";
import { Feature, FeatureValueType } from '../../models';
import { Text, Toggle } from '@fluentui/react';

type Props = {
    feature: Feature,
    checked: boolean,
    handleFeatureChange: (feature: Feature, newValue: FeatureValueType) => void
};

const FeatureToggle: FunctionalComponent<Props> = (props) => {
    const {
        feature,
        checked,
        handleFeatureChange
    } = props;

    const {
        readonly,
        uiPrefix,
        uiSuffix
    } = feature;

    return (
        <div>
            <p>
                <Text block variant="large">
                    {feature.name}
                </Text>
                {feature.description &&
                    <Text block variant="small">
                        {feature.description}
                    </Text>
                }
            </p>
            <Toggle
                checked={checked}
                disabled={readonly}
                onChange={() => handleFeatureChange(feature, !checked)}
                prefix={uiPrefix || undefined}
                suffix={uiSuffix || undefined}
            />
        </div>
    );
};

export default FeatureToggle;

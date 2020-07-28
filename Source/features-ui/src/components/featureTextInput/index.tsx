// @ts-nocheck
import { FunctionalComponent, h } from "preact";
import { Feature, FeatureValueType } from '../../models';
import { Text, TextField, PrimaryButton, DefaultButton } from '@fluentui/react';
import { useState } from "preact/hooks";

type Props = {
    feature: Feature,
    value: string,
    handleFeatureChange: (feature: Feature, newValue: FeatureValueType) => void
};

const FeatureTextInput: FunctionalComponent<Props> = (props) => {
    const {
        feature,
        value,
        handleFeatureChange
    } = props;

    const { readonly } = feature;

    const [newValue, setNewValue] = useState(value);

    const hasChanged = value !== newValue;
    
    const canSave = hasChanged;
    const canCancel = hasChanged;

    const onTextChanged = (e: React.KeyboardEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const target = e.target as HTMLInputElement;
        setNewValue(target.value || '');
    }

    const onValidateButtonClicked = () => {
        handleFeatureChange(feature, newValue);
    }

    const onCancelButtonClicked = () => {
        setNewValue(value);
    }

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
            <TextField
                defaultValue={newValue}
                disabled={readonly}
                onKeyUp={onTextChanged}
            />
            <p>
                {canSave && (
                    <PrimaryButton 
                        text="Save" 
                        onClick={() => onValidateButtonClicked()} 
                        allowDisabledFocus
                    />
                )}
                {canCancel && (
                    <DefaultButton 
                        text="Cancel" 
                        onClick={() => onCancelButtonClicked()} 
                        allowDisabledFocus
                        style={{ marginLeft: 12 }}
                    />
                )}
            </p>
        </div>
    );
};

export default FeatureTextInput;

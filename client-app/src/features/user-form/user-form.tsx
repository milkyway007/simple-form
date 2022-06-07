import { Formik, Form, Field, ErrorMessage } from "formik";
import {
  ReactNode,
} from "react";
import { SectorOption } from "../../app/models/sector-option";
import { User } from "../../app/models/user";
import * as Yup from 'yup';

interface Props {
  sectorOptions: SectorOption[];
  user: User;
  update: (user: User) => void;
  submitting: boolean;
}

export default function UserForm({
  sectorOptions,
  user,
  update,
}: Props) {
  const validationSchema = Yup.object({
    name: Yup.string().required("The user name is required."),
    sectorOptionIds: Yup.array().min(1, 'At least one sector option is required.'),
    isAgreed: Yup.bool().isTrue("Agreement is required."),
  });

  function handleFormSubmit(
    user: User,
    setSubmitting: { (isSubmitting: boolean): void; (arg0: boolean): void; }) {
    update(user);
    setSubmitting(false);
  }

  function show() {
    const select = document.querySelector("select");
    if (!select) {
      return;
    }

    select.classList.remove("hide");
  }

  function renderNode(option: SectorOption) {
    const className = `level-${option.level}`;
    if (option.children != null && option.children.length > 0) {
      return (
        <optgroup
          key={option.id}
          label={option.label}
          className={className}
        ></optgroup>
      );
    } else {
      return (
        <option key={option.id} value={option.id} className={className}>
          {option.label}
        </option>
      );
    }
  }

  function renderNodes(option: SectorOption, nodes: ReactNode[]) {
    nodes.push(renderNode(option));
    if (option.children == null) {
      return;
    }
    for (const child of option.children) {
      renderNodes(child, nodes);
    }
  }

  return (
    <div>
      <Formik
        validationSchema={validationSchema}
        initialValues={user}
        onSubmit={(values, { setSubmitting }) => handleFormSubmit(values, setSubmitting)}
      >
        {({ handleSubmit, isValid, isSubmitting, dirty }) => (
          <Form onSubmit={handleSubmit} autoComplete="off">
            <div>
              <label htmlFor="name">Name:</label>
              <Field name="name" />
              <ErrorMessage
                name="name"
                render={(error) => (
                  <label className="error-label">{error}</label>
                )}
              ></ErrorMessage>
            </div>

            <div>
              <label htmlFor="sectors">Sectors:</label>
              <Field
                component="select"
                name="sectorOptionIds"
                size={5}
                className={"hide"}
                multiple={true}
              >
                {sectorOptions.map((option) => {
                  let nodes: ReactNode[] = [];
                  renderNodes(option, nodes);
                  show();
                  return nodes;
                })}
              </Field>
              <ErrorMessage
                name="sectorOptionIds"
                render={(error) => (
                  <label className="error-label">{error}</label>
                )}
              ></ErrorMessage>
            </div>

            <div>
              <label htmlFor="isAgreed">Agree to terms</label>
              <Field type="checkbox" name="isAgreed" />
              <ErrorMessage
                name="isAgreed"
                render={(error) => (
                  <label className="error-label">{error}</label>
                )}
              ></ErrorMessage>
            </div>
            <div>
              <input
                type="submit"
                value="Save"
                disabled={isSubmitting || !dirty || !isValid} />
            </div>
          </Form>
        )}
      </Formik>
    </div>
  );
}

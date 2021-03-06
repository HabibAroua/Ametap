﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MetroFramework.Forms;
using AMETAP.Controller;
using AMETAP.Model.Business;
using Tulpep.NotificationWindow;
using AMETAP.Model.DataAcces;

namespace AMETAP.View.Gestion_des_activites
{
    public partial class AjouterActivite : MetroForm
    {
        ActiviteController ac;
        TypeActiviteController taC;
        OrganisateurController oc;
        BudgetCategorieController bcc;
        public AjouterActivite()
        {
            InitializeComponent();
            ac = new ActiviteController();
            taC = new TypeActiviteController();
            oc = new OrganisateurController();
            bcc = new BudgetCategorieController();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if(rdList.Checked==true)
                {
                    if (int.Parse(viewMontant.Text.ToString()) - int.Parse(txtMontantPrevu.Text.ToString()) >= 0)
                    {
                        if (rdActiviteCulturel.Checked == true)
                        {
                            ac.AjouterActivite(comboNomActivite.SelectedItem.ToString(), int.Parse(txtCapacite.Text.ToString()), txtDateDebut.Text.ToString(), txtDatefin.Text.ToString(), double.Parse(txtPrixUnitaire.Text.ToString()), double.Parse(txtMontantPrevu.Text.ToString()), double.Parse(txtMontantPrevu.Text.ToString()), "Activité culturel", comboOrganisateur.SelectedItem.ToString(), txtDateDebutInscription.Text.ToString(), dateFinInscription.Text.ToString(), int.Parse(comboNbr_point.SelectedItem.ToString()));
                            PopupNotifier pn = new PopupNotifier();
                            pn.Image = Properties.Resources.Information;
                            pn.TitleText = "Information";
                            pn.ContentText = "Les emails sont envoyées avec succes pour s'informer les adhérents";
                            pn.Popup();
                        }
                        else
                        {
                            if (rdActiviteLoisir.Checked == true)
                            {
                                ac.AjouterActivite(comboNomActivite.SelectedItem.ToString(), int.Parse(txtCapacite.Text.ToString()), txtDateDebut.Text.ToString(), txtDatefin.Text.ToString(), double.Parse(txtPrixUnitaire.Text.ToString()), double.Parse(txtMontantPrevu.Text.ToString()), double.Parse(txtMontantPrevu.Text.ToString()), "Activité de loisir", comboOrganisateur.SelectedItem.ToString(), txtDateDebutInscription.Text.ToString(), dateFinInscription.Text.ToString(), int.Parse(comboNbr_point.SelectedItem.ToString()));
                                viewMontant.Text = bcc.getMontantProvisoire("Activité de loisir").ToString();
                                PopupNotifier pn = new PopupNotifier();
                                pn.Image = Properties.Resources.Information;
                                pn.TitleText = "Information";
                                pn.ContentText = "Les emails sont envoyées avec succes pour s'informer les adhérents";
                                pn.Popup();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vous n'avez pas ajouter une nouvelle activité car le budget est inssuffisant !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    if(rdManuel.Checked==true)
                    {
                        if (int.Parse(viewMontant.Text.ToString()) - int.Parse(txtMontantPrevu.Text.ToString()) >= 0)
                        {
                            if (rdActiviteCulturel.Checked == true)
                            {
                                ac.AjouterActivite(txtNomActivite.Text.ToString(), int.Parse(txtCapacite.Text.ToString()), txtDateDebut.Text.ToString(), txtDatefin.Text.ToString(), double.Parse(txtPrixUnitaire.Text.ToString()), double.Parse(txtMontantPrevu.Text.ToString()), double.Parse(txtMontantPrevu.Text.ToString()), "Activité culturel", comboOrganisateur.SelectedItem.ToString(), txtDateDebutInscription.Text.ToString(), dateFinInscription.Text.ToString(), int.Parse(comboNbr_point.SelectedItem.ToString()));

                            }
                            else
                            {
                                if (rdActiviteLoisir.Checked == true)
                                {
                                    ac.AjouterActivite(txtNomActivite.Text.ToString(), int.Parse(txtCapacite.Text.ToString()), txtDateDebut.Text.ToString(), txtDatefin.Text.ToString(), double.Parse(txtPrixUnitaire.Text.ToString()), double.Parse(txtMontantPrevu.Text.ToString()), double.Parse(txtMontantPrevu.Text.ToString()), "Activité de loisir", comboOrganisateur.SelectedItem.ToString(), txtDateDebutInscription.Text.ToString(), dateFinInscription.Text.ToString(), int.Parse(comboNbr_point.SelectedItem.ToString()));
                                    viewMontant.Text = bcc.getMontantProvisoire("Activité de loisir").ToString();

                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Vous n'avez pas ajouter une nouvelle activité car le budget est inssuffisant !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                 
                }
                this.Close();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Il y a des champs vides", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AjouterActivite_Load(object sender, EventArgs e)
        {
            try
            {
                txtNomActivite.Enabled = false;
                comboNomActivite.Enabled = false;
                dt.Hide();
                foreach (Organisateur o in oc.AllOrganisateur())
                {
                    comboOrganisateur.Items.Add(o.nom_organisateur);
                }
                txtMontantPrevu.Enabled = false;
                comboNbr_point.Items.Add("10");
                comboNbr_point.Items.Add("20");
                comboNbr_point.Items.Add("30");
                comboNbr_point.Items.Add("40");
                comboNbr_point.Items.Add("50");
                comboNbr_point.Items.Add("60");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCapacite_ValueChanged(object sender, EventArgs e)
        {
            if ((txtPrixUnitaire.Text.Equals("")) || (txtCapacite.Text.Equals("0")))
            {
                txtMontantPrevu.Text = "";
            }
            else
            {
                txtMontantPrevu.Text = "" + (int.Parse(txtPrixUnitaire.Text) / 2) * int.Parse(txtCapacite.Text);
            }
        }

        private void txtPrixUnitaire_TextChanged(object sender, EventArgs e)
        {
            if ((txtPrixUnitaire.Text.Equals("")) || (txtCapacite.Text.Equals("0")))
            {
                txtMontantPrevu.Text = "";
            }
            else
            {
                txtMontantPrevu.Text = "" + (int.Parse(txtPrixUnitaire.Text) / 2) * int.Parse(txtCapacite.Text);
            }
        }
        private void rdActiviteCulturel_CheckedChanged(object sender, EventArgs e)
        {
            comboNomActivite.Items.Clear();
            if (rdActiviteCulturel.Checked == true)
            {
                comboNomActivite.Items.Add("Cours Anglais");
                comboNomActivite.Items.Add("Cours francais");
                comboNomActivite.Items.Add("Cours Espagnole");
                comboNomActivite.Items.Add("Theatre");
                comboNomActivite.Items.Add("Cinema");
            }
        }

        private void rdActiviteLoisir_CheckedChanged(object sender, EventArgs e)
        {
            comboNomActivite.Items.Clear();
            if (rdActiviteLoisir.Checked == true)
            {
                comboNomActivite.Items.Add("Cours aerobic");
                comboNomActivite.Items.Add("Cours danse orientale");
                comboNomActivite.Items.Add("Omra");
                comboNomActivite.Items.Add("Voyage Japon");
                comboNomActivite.Items.Add("Voyage France");
                comboNomActivite.Items.Add("Voyage Espagne");
                comboNomActivite.Items.Add("Voyage Italie");
                comboNomActivite.Items.Add("Voyage Allemagne");
                comboNomActivite.Items.Add("Voyage Autriche");
            }
        }

        private void rdActiviteCulturel_Click(object sender, EventArgs e)
        {
            viewMontant.Text = bcc.getMontantProvisoire("Activité culturel").ToString();
            dt.Show();
        }

        private void rdActiviteLoisir_Click(object sender, EventArgs e)
        {
            viewMontant.Text = "" + bcc.getMontantProvisoire("Activité de loisir").ToString();
            dt.Show();
        }

        private void btAnnuler_Click(object sender, EventArgs e)
        {
            AdherentDA aa = new AdherentDA();
            List<String> list = aa.listAdresse();
            foreach(String c in list)
            {
                MessageBox.Show(c);
            }
            DialogResult a = MessageBox.Show("Voulez vous quitter ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void comboNomActivite_Click(object sender, EventArgs e)
        {
            if((rdActiviteCulturel.Checked==false)&&(rdActiviteLoisir.Checked==false))
            {
                MessageBox.Show("Vous devez séléctionner la catégorie", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void rdManuel_Click(object sender, EventArgs e)
        {
            txtNomActivite.Enabled = true;
            comboNomActivite.Enabled = false;
        }

        private void rdList_Click(object sender, EventArgs e)
        {
            comboNomActivite.Enabled = true;
            txtNomActivite.Enabled = false;
        }
    }
}

